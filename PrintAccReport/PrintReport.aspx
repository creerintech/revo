<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReport.aspx.cs" Inherits="PrintAccReport_PrintReport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Reports</title>   
<script type="text/javascript">
    function fixform1() 
    {
    
        if (opener.document.getElementById("aspnetForm").target != "_blank") return;
        opener.document.getElementById("aspnetForm").target = "";
        opener.document.getElementById("aspnetForm").action = opener.location.href;n.href;
       
    }
</script>

</head>
<body onload ="fixform1();">
    <form id="form12" runat="server">
    <div>
    <center>
      <%--  <CR:CrystalReportViewer ID="CRPrint" runat="server" AutoDataBind="true"
        EnableDatabaseLogonPrompt="false" EnableParameterPrompt="false"
        ReuseParameterValuesOnRefresh="true" DisplayGroupTree="False"/>--%>

        <CR:CrystalReportViewer ID="CRPrint" runat="server" AutoDataBind="true"
        EnableDatabaseLogonPrompt="false" EnableParameterPrompt="false"
        ReuseParameterValuesOnRefresh="true" />
    </center>
    </div>
    </form>
</body>
</html>

