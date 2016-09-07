﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;

namespace JXDL.ClientBusiness
{
    public class CommonUnit
    {
        public static string ConvertLayerType(int type)
        {
            string vResult = "";
            switch (type)
            {
                case 0:
                    vResult = "点";
                    break;
                case 1:
                    vResult = "线";
                    break;
                case 2:
                    vResult = "面";
                    break;
            }
            return vResult;
        }

        public static Type ConvertFeaturesFieldType(esriFieldType FieldType )
        {
            Type vType = null;
            switch (FieldType)
            {
                case esriFieldType.esriFieldTypeSmallInteger:
                    vType = typeof(Int16);
                    break;
                case esriFieldType.esriFieldTypeInteger:
                    vType = typeof(Int32);
                    break;
                case esriFieldType.esriFieldTypeSingle:
                    vType = typeof(float);
                    break;
                case esriFieldType.esriFieldTypeDouble:
                    vType = typeof(double);
                    break;
                case esriFieldType.esriFieldTypeString:
                    vType = typeof(string);
                    break;
                case esriFieldType.esriFieldTypeDate:
                    vType = typeof(DateTime);
                    break;
                case esriFieldType.esriFieldTypeOID:
                    vType = typeof(int);
                    break;
                case esriFieldType.esriFieldTypeGeometry:
                    vType = typeof(object);
                    break;
                case esriFieldType.esriFieldTypeBlob:
                    vType = typeof(byte[]);
                    break;
                case esriFieldType.esriFieldTypeRaster:
                    vType = typeof(byte[]);
                    break;
                case esriFieldType.esriFieldTypeGUID:
                case esriFieldType.esriFieldTypeGlobalID:
                case esriFieldType.esriFieldTypeXML:
                    vType = typeof(string);
                    break;
            }
            return vType;
        }
    }
}