﻿using Microsoft.Practices.ServiceLocation;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuperTankWPF.Util
{
    class Comunication : IDisposable
    {
        private ChannelFactory<IKeyboard> factoryKeyboard;
        private ServiceHost hostGameInfo;
        private ServiceHost hostSceneView;
        private ServiceHost hostSound;
        private SynchronizationContext synchronizationContext;

        public Comunication(SynchronizationContext synchronizationContext)
        {
            this.synchronizationContext = synchronizationContext;
        }

        public void OpenHost()
        {
            synchronizationContext.Post((s) =>
            {
                hostSound = new ServiceHost(ServiceLocator.Current.GetInstance<ISoundGame>());
                hostSound.CloseTimeout = TimeSpan.FromMilliseconds(50);
                hostSound.AddServiceEndpoint(typeof(ISoundGame), new NetNamedPipeBinding(), "net.pipe://localhost/ISoundGame");
                hostSound.Open();

                hostGameInfo = new ServiceHost(ServiceLocator.Current.GetInstance<GameMenedger>());
                hostGameInfo.CloseTimeout = TimeSpan.FromMilliseconds(50);
                hostGameInfo.AddServiceEndpoint(typeof(IGameInfo), new NetNamedPipeBinding(), "net.pipe://localhost/IGameInfo");
                hostGameInfo.Open();
            }, null);

            hostSceneView = new ServiceHost(ServiceLocator.Current.GetInstance<ScrenSceneViewModel>());
            hostSceneView.CloseTimeout = TimeSpan.FromMilliseconds(50);
            hostSceneView.AddServiceEndpoint(typeof(IRender), new NetNamedPipeBinding(), "net.pipe://localhost/IRender");
            hostSceneView.Open();

        }
        public IKeyboard OpenChannel()
        {
            factoryKeyboard = new ChannelFactory<IKeyboard>(new NetNamedPipeBinding(), "net.pipe://localhost/IKeyboard");
            factoryKeyboard.Open(TimeSpan.FromSeconds(1));
            return factoryKeyboard.CreateChannel();
        }

        public void CloseHost()
        {
            hostSound?.Close();
            hostSceneView?.Close();
            hostGameInfo?.Close();
        }

        public void CloseChannelFactory()
        {
            factoryKeyboard?.Close();
        }

        public void Dispose()
        {
            CloseHost();
            CloseChannelFactory();
        }
    }
}