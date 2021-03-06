﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using JXDL.IntrefaceStruct;
using System.Web.Script.Serialization;

namespace JXDL.ClientBusiness
{

    public class LayerConfig
    {
        public List<LayerInfo> Config { get; set; } = new List<LayerInfo>();
    }

    public class LayerInfo
    {
        public string UserName { get; set; }
        public LayerStruct[] Layers { get; set; } 
    }


    public class ConfigFile
    {
        #region 私有变量

        Configuration m_Configuration = null;
        /// <summary>
        /// 远程服务器地址
        /// </summary>
        public string RemotingServerAddress { get; set; }
        /// <summary>
        /// 使用符号库
        /// </summary>
        public bool UseSymbol { get; set; } = true;
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
        //public Dictionary<string, int> LayerColor { get; set; } = new Dictionary<string, int>();
        LayerConfig LayerConfig;
        public LayerInfo GetLayerInfo(string UserName)
        {
            LayerInfo vLayerInfo = LayerConfig.Config.Where(m => m.UserName.ToUpper() == UserName.ToUpper()).FirstOrDefault();
            return vLayerInfo;
        }

        public void  SetLayerInfo(LayerInfo LayerInfoValue )
        {
            LayerInfo vLayerInfo = LayerConfig.Config.Where(m => m.UserName.ToUpper() == LayerInfoValue.UserName.ToUpper()).FirstOrDefault();
            if ( vLayerInfo != null )
            {
                LayerConfig.Config.Remove(vLayerInfo);
            }
            LayerConfig.Config.Add(LayerInfoValue);
            
        }

        #endregion


        #region 构造
        public ConfigFile()
        {
            m_Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            RemotingServerAddress = m_Configuration.AppSettings.Settings["RemotingServerAddress"].Value;
            UseSymbol = Convert.ToBoolean( m_Configuration.AppSettings.Settings["UseSymbol"].Value );
            MapBackgroundColor    = int.Parse( m_Configuration.AppSettings.Settings["MapBackgroundColor"].Value );
            TownshipBackgroundColor = int.Parse(m_Configuration.AppSettings.Settings["TownshipBackgroundColor"].Value);
            VillageCommitteeBackgroundColor = int.Parse(m_Configuration.AppSettings.Settings["VillageCommitteeBackgroundColor"].Value);
            VillageBackgroundColor = int.Parse(m_Configuration.AppSettings.Settings["VillageBackgroundColor"].Value);
            DownloadPath = m_Configuration.AppSettings.Settings["DownloadPath"].Value;

            //string vLayerColor = m_Configuration.AppSettings.Settings["LayerColor"].Value;
            //string[] vLayerArray = vLayerColor.Split('|');
            //foreach( string vTempLayer in vLayerArray)
            //{
            //    string[] vLayerData = vTempLayer.Split(',');
            //    if (vLayerData.Length == 2 )
            //    {
            //        if (LayerColor.ContainsKey(vLayerData[0]))
            //            LayerColor[vLayerData[0]] = int.Parse( vLayerData[1] );
            //        else
            //        {
            //            LayerColor.Add(vLayerData[0], int.Parse( vLayerData[1] ));
            //        }
            //    }
            //}

            string vLayerConfig = m_Configuration.AppSettings.Settings["LayerConfig"].Value;
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            LayerConfig = vJSC.Deserialize< LayerConfig>(vLayerConfig);
            if (LayerConfig == null)
                LayerConfig = new LayerConfig();
        }
        #endregion

        #region 公有方法
        public void Save()
        {
            //远程服务器
            m_Configuration.AppSettings.Settings["RemotingServerAddress"].Value = RemotingServerAddress;
            m_Configuration.AppSettings.Settings["UseSymbol"].Value = UseSymbol.ToString();
            m_Configuration.AppSettings.Settings["MapBackgroundColor"].Value = MapBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["TownshipBackgroundColor"].Value = TownshipBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["VillageCommitteeBackgroundColor"].Value = VillageCommitteeBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["VillageBackgroundColor"].Value = VillageBackgroundColor.ToString();
            m_Configuration.AppSettings.Settings["DownloadPath"].Value = DownloadPath;

            //string vLayerColor = "";
            //foreach ( var vTempLayerColor in LayerColor)
            //{
            //    if (vLayerColor == "")
            //        vLayerColor += string.Format("{0},{1}", vTempLayerColor.Key, vTempLayerColor.Value);
            //    else
            //        vLayerColor += string.Format("|{0},{1}", vTempLayerColor.Key, vTempLayerColor.Value);
            //}
            //m_Configuration.AppSettings.Settings["LayerColor"].Value = vLayerColor;
            JavaScriptSerializer vJSC = new System.Web.Script.Serialization.JavaScriptSerializer();
            m_Configuration.AppSettings.Settings["LayerConfig"].Value = vJSC.Serialize(LayerConfig);
            m_Configuration.Save(ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion

        
    }
}
