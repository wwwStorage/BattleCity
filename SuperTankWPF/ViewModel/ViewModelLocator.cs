﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using SuperTank;
using SuperTank.Audio;
using SuperTank.View;
using SuperTankWPF.Model;
using SuperTankWPF.Units;
using SuperTankWPF.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankWPF.ViewModel
{
    class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<StartScrenViewModel>();
            SimpleIoc.Default.Register<ScrenScoreViewModel>();
            SimpleIoc.Default.Register<LevelInfoViewModel>(() => new LevelInfoViewModel());
            SimpleIoc.Default.Register<ScrenGameViewModel>();
            SimpleIoc.Default.Register<ScrenRecordViewModel>();

            SimpleIoc.Default.Register<ScrenSceneViewModel>();
            SimpleIoc.Default.Register<GameMenedger>();
            SimpleIoc.Default.Register<IImageSourceStor, ImageSourceStor>();
            SimpleIoc.Default.Register<IFactoryUnitView, FactoryUnitView>();

            SimpleIoc.Default.Register<ISoundGame>(() => new SoundGame(ConfigurationWPF.SoundPath));

            SimpleIoc.Default.Register<Comunication>();
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public StartScrenViewModel StartScren
        {
            get
            {
                return ServiceLocator.Current.GetInstance<StartScrenViewModel>();
            }
        }
        public ScrenScoreViewModel ScrenScore
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenScoreViewModel>();
            }
        }
        public LevelInfoViewModel LevelInfoLeve
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LevelInfoViewModel>();
            }
        }
        public ScrenGameViewModel ScrenGame
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenGameViewModel>();
            }
        }
        public ScrenRecordViewModel ScrenRecord
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenRecordViewModel>();
            }
        }
        public ScrenSceneViewModel ScrenScene
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ScrenSceneViewModel>();
            }
        }
        public IImageSourceStor ImageSource
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IImageSourceStor>();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            //ServiceLocator.Current.GetInstance<Comunication>().Dispose();
        }
    }
}
