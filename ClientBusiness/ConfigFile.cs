﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;


namespace JXDL.ClientBusiness
{
    public class ConfigFile
    {
        #region 私有变量

        Configuration m_Configuration = null;
        /// <summary>
        /// 远程服务器地址
        /// </summary>
        public string RemotingServerAddress { get; set; }
        /// <summary>
        /// 文档下载路径
        /// </summary>
        public string DownloadPath { get; set; }
        /// <summary>
        /// 地图背景颜色
        /// </summary>
        public int MapBackgroundColor { get; set; }

        public int TownshipBackgroundColor { get; set; }
        public int VillageCommitteeBackgroundColor { get; set; }
        public int VillageBackgroundColor { get; set; }


        #endregion

        #region 构造
        public ConfigFile()
        {
            m_Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            RemotingServerAddress = m_Configuration.AppSettings.Settings["RemotingServerAddress"].Value;
            MapBackgroundColor = int.Parse( m_Configuration.AppSettings.Settings["MapBackgroundColor"].Value );
            TownshipBackgroundColor = int.Parse(m_Configuration.AppSettings.Settings["TownshipBackgroundColor"].Value);
            VillageCommitteeBackgroundColor = int.Parse(m_Configuration.AppSettings.Settings["VillageCommitteeBackgroundColor"].Value);
            VillageBackgroundColor = int.Parse(m_Configuration.AppSettings.Settings["VillageBackgroundColor"].Value);
            DownloadPath = m_Configuration.AppSettings.Settings["DownloadPath"].Value;
        }
        #endregion

        #region 公有方法
        public void Save()
        {
            //远程服务器
            m_Configuration.AppSettings.Settings["RemotingServerAddress"].Value = RemotingServerAddress;
            m_Configuration.AppSettings.Settings["MapBackgroundColor"].Value = MapBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["TownshipBackgroundColor"].Value = TownshipBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["VillageCommitteeBackgroundColor"].Value = VillageCommitteeBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["VillageBackgroundColor"].Value = VillageBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["DownloadPath"].Value = DownloadPath;
            m_Configuration.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        
    }
}
