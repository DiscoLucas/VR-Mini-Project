using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Linq;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Lobbies.Models;
using Unity.Services.Lobbies;

public class ConnectToNetwork : MonoBehaviour
{
    Guid hostAllocationId;
    Guid playerAllocationId;
    string allocationRegion = "";
    string relayCode = "n/a";
    string playerId = "Not signed in";
    string autoSelectRegionName = "auto-select (QoS)";
    int regionAutoSelectIndex = 0;
    List<Region> regions = new List<Region>();
    List<string> regionOptions = new List<string>();

    private UnityTransport transport;

    [SerializeField] private int maxConnections = 2;

    async void Start()
    {
        transport = GetComponent<UnityTransport>();

        await UnityServices.InitializeAsync();

        OnSignIn();
    }


    /// <summary>
    /// Event handler for when the Sign In button is clicked.
    /// </summary>
    public async void OnSignIn()
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        playerId = AuthenticationService.Instance.PlayerId;

        Debug.Log($"Signed in. Player ID: {playerId}");
    }




    public async void StartHost()
    {
        // Important: Once the allocation is created, you have ten seconds to BIND
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnections);
        hostAllocationId = allocation.AllocationId;
        allocationRegion = allocation.Region;

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        try
        {
            relayCode = await RelayService.Instance.GetJoinCodeAsync(hostAllocationId);
            Debug.Log("Host - Got join code: " + relayCode);
            var dataObject = new DataObject(DataObject.VisibilityOptions.Public, relayCode);
            var data = new Dictionary<string, DataObject>();
            data.Add("JOIN_CODE: ", dataObject);

            var lobbyOptions = new CreateLobbyOptions
            {
                IsPrivate = false,
                Data = data
            };

            await Lobbies.Instance.CreateLobbyAsync("LOBBY TEST", maxConnections, lobbyOptions);
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException ex)
        {
            Debug.LogError(ex.Message + "\n" + ex.StackTrace);
        }

    }

    public async void StartClient()
    {

        try
        {
            var lobby = await Lobbies.Instance.QuickJoinLobbyAsync();

            relayCode = lobby.Data["JOIN_CODE: "].Value;

            var joinAllocation = await RelayService.Instance.JoinAllocationAsync(relayCode);
            playerAllocationId = joinAllocation.AllocationId;

            transport.SetClientRelayData(joinAllocation.RelayServer.IpV4, (ushort)joinAllocation.RelayServer.Port, joinAllocation.AllocationIdBytes, joinAllocation.Key, joinAllocation.ConnectionData, joinAllocation.HostConnectionData);

            NetworkManager.Singleton.StartClient();
            Debug.Log("Player Allocation ID: " + playerAllocationId);
        }
        catch (RelayServiceException ex)
        {
            Debug.LogError(ex.Message + "\n" + ex.StackTrace);
        }



    }

}
