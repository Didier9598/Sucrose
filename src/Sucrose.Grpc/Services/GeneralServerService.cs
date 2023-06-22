﻿using Grpc.Core;
using Sucrose.Grpc.Helper;
using System.Collections.Generic;
using System.Net;

namespace Sucrose.Grpc.Services
{
    public static class GeneralServerService
    {
        public static IPAddress Host = IPAddress.Loopback;
        public static int Port = Gadget.AvailablePort(Host);

        public static Server ServerInstance { get; set; }
        public static Channel ChannelInstance { get; set; }

        public static void ServerCreate(ServerServiceDefinition Services)
        {
            ServerInstance = new Server
            {
                Services =
                {
                    Services
                },
                Ports =
                {
                    new ServerPort(Host.ToString(), Port, ServerCredentials.Insecure)
                }
            };
        }

        public static void ServerCreate(IEnumerable<ServerServiceDefinition> Services)
        {
            ServerInstance = new Server
            {
                Ports =
                {
                    new ServerPort(Host.ToString(), Port, ServerCredentials.Insecure)
                }
            };

            foreach (ServerServiceDefinition Service in Services)
            {
                ServerInstance.Services.Add(Service);
            }
        }

        public static void ChannelCreate()
        {
            ChannelInstance = new($"{Host}:{Port}", ChannelCredentials.Insecure);
        }
    }
}
