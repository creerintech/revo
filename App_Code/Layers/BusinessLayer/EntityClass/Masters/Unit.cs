﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Unit
/// </summary>
public class Unit
{
    #region[Variables]
    public static string _Action = "@Action";
    public static string _UnitId = "@UnitId";
    public static string _Unit = "@Unit";
    public static string _UserId = "@UserId";
    public static string _LoginDate = "@LoginDate";
    public static string _IsDeleted = "@IsDeleted";
    public static string _StrCondition = "@strCond";
    #endregion

    #region[Definations]
    //Action
    private Int32 m_Action;

    public Int32 Action
    {
        get { return m_Action; }
        set { m_Action = value; }
    }

    //UnitId
    private Int32 m_UnitId;
    public Int32 UnitId
    {
        get { return m_UnitId; }
        set { m_UnitId = value; }
    }

    //Unit
    private string m_Unit;
    public string UnitName
    {
        get { return m_Unit; }
        set { m_Unit = value; }
    }

    //UserId
    private Int32 m_UserId;
    public Int32 UserId
    {
        get { return m_UserId; }
        set { m_UserId=value; }
    }

    //LoginDate
    private DateTime m_LoginDate;
    public DateTime LoginDate
    {
        get { return  m_LoginDate; }
        set { m_LoginDate = value; }
    }

    //IsDeleted
    private bool m_IsDeleted;
    public bool IsDeleted
    {
        get { return m_IsDeleted; }
        set { m_IsDeleted = value; }
    }
     
    //StrCondition

    private string m_StrCondition;
    public string StrCondition
    {
        get { return m_StrCondition; }
        set { m_StrCondition = value; }
    }

    #endregion

    #region[Procedure]
    public static string SP_UnitMaster = "SP_UnitMaster";
    #endregion


    public Unit()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}