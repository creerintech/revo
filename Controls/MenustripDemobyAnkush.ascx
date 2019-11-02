<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenustripDemobyAnkush.ascx.cs"
    Inherits="Controls_MenustripDemobyAnkush" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<nav>
	<ul>
		<li><a href="../Masters/Default.aspx">Home</a></li>
		<%--<li><a id="A01" href="#" runat="server">Masters</a>--%>
		<li><a  href="#">Masters</a>
			<ul>

			    <li><a id="A1" href="../Masters/UserMaster.aspx" runat="server" >User Master</a></li>
			    <li><a id="A60" href="~/Masters/TaxMaster.aspx" runat="server" >Tax Master</a></li>
			    <li><a id="A38" href="../Masters/CompanyMaster.aspx" runat="server" >Company Master</a></li>
			    <li><a id="A65" href="../Masters/ClientCompanyMaster.aspx" runat="server" >Company Representer (Supplier)</a></li>
				<li><a id="A2" href="../Masters/SupplierMaster.aspx" runat="server" >Supplier Master</a></li>
				<li><a id="A501" href="../Masters/Customermaster.aspx" runat="server" >Customer Master</a></li>
				<li><a id="A3" href="../Masters/EmployeeMaster.aspx" runat="server" >Employee Master</a></li>
                	<li><a id="A71" href="../Masters/Project_master.aspx" runat="server" >Project Master</a></li>

                <li><a id="A85" href="../Masters/TagPlatemaster.aspx" runat="server" >Tag Plate  Master</a></li>

                 <li><a id="A86" href="../Masters/woodenbox.aspx" runat="server" >Wooden Size Master  </a></li>
                 <li><a id="A87" href="../Masters/Non-stditemshardware.aspx" runat="server" >Non-stditemshardware Master  </a></li>

          
                	<li><a id="A731" href="../Masters/systeamname.aspx" runat="server" >System Name Master</a></li>
                	<li><a id="A73" href="../Masters/valvetype.aspx" runat="server" >Valve Type</a></li>
                	<li><a id="A77" href="../Masters/Valvesize.aspx" runat="server" >Valve Size</a></li>
                	<li><a id="A78" href="../Masters/valveclass.aspx" runat="server" >Valve Class</a></li>
                	<li><a id="A82" href="../Masters/vlaveoperator.aspx" runat="server" >Valve Operator</a></li>
                	<li><a id="A81" href="../Masters/interlocks.aspx" runat="server" >Interlock</a></li>
                	<li><a id="A80" href="../Masters/hanwheelsize.aspx" runat="server" >Handwheel Size</a></li>
                	<li><a id="A79" href="../Masters/lever.aspx" runat="server" >Lever Size</a></li>










				<li><a id="A4" href="../Masters/StockLocationMaster.aspx" runat="server" >Site Master</a></li>
				<li><a id="A57" href="~/Masters/TowerMaster.aspx" runat="server" >Tower Master</a></li>
				<li><a id="A45" href="~/Masters/CostLocationMaster.aspx" runat="server" >Cost Centre Master</a></li>
				<li><a id="A5" href="../Masters/ItemCategory.aspx" runat="server" >Item Category Master</a></li>
				<li><a id="A7" href="../Masters/ItemSubCategoryMaster.aspx" runat="server" >Item Sub Category Master</a></li>
				<li><a id="A6" href="../Masters/UnitMaster.aspx" runat="server" >Unit Master</a></li>
				<li><a id="A44" href="~/Masters/UnitConversionMaster.aspx" runat="server" >Unit Conversion Master</a></li>
				<li><a id="A42" href="../Masters/ItemSize.aspx" runat="server" >Item Size Master</a></li>
			<li><a id="A8" href="../Masters/ItemMaster.aspx" runat="server" >Item Master</a></li>
				<li><a id="A25" href="../Masters/ProrityMaster.aspx" runat="server" >Priority Master</a></li>
				<li><a id="A24" href="~/Masters/TermsCondition.aspx" runat="server" >Terms Condition Master</a></li>
				
				<li><a id="A66" href="~/Masters/CHANGESTATUSPOIND.aspx" runat="server" >Change Status</a></li>
				
			</ul>
		</li>
		<%--<li><a id="A02" href="#" runat="server">Transactions</a>--%>
		<li><a href="#">Transactions</a>
			<ul>
				<li><a id="A29"  href="~/Transactions/Template.aspx" runat="server" >Master list of drawings</a></li>	

                	<li><a id="A84"  href="~/Transactions/newnonstdlist.aspx" runat="server" >Non Std (B.O.M)</a></li>	


                	<li><a id="A83"  href="~/Transactions/Projectscope1.aspx" runat="server" > Project Scope</a></li>
				<li><a id="A9"  href="~/Transactions/MaterialRequisitionTemplate.aspx" runat="server" >Indent Generation</a></li>
				<li><a id="A9n"  href="~/Transactions/nonstdindentaspx.aspx" runat="server" > Non Std Indent Generation</a></li>
                	<li><a id="A31" href="~/Transactions/ApproveMatRequisition.aspx" runat="server" >Indent Authorization</a></li>	
                				<li><a id="A67"  href="~/Transactions/Generateenquiry.aspx" runat="server" >Generate Enquiry</a></li>
                	<li><a id="A68"  href="~/Transactions/supplierenquiry.aspx" runat="server" >Supplier quotation</a></li>
                 	<li><a id="A70"  href="~/Transactions/conversion.aspx" runat="server" >Supplier Comparsion Sheet </a></li>


                	<li><a id="A74"  href="~/Transactions/approvecomparisionsheet.aspx" runat="server" >Approve Comparison  Sheet</a></li>
                	<li><a id="A69"  href="~/Transactions/Estimate.aspx" runat="server" >Estimate</a></li>
                	<li><a id="A72"  href="~/Transactions/ApproveEstimate.aspx" runat="server" >Approve Estimate</a></li>





                
                
				<%--<li><a id="A10" href="~/Transactions/MaterialRequistnCancel.aspx" runat="server" >Indent Cancellation</a></li>	--%>			
			
			<li><a id="A100" href="~/Transactions/EmailIndent.aspx" runat="server" >Indent Follow Up</a></li>	
				<li><a id="A11" href="../Transactions/PurchaseOrderDtls.aspx" runat="server" >Purchase Order Generation</a></li>
				<li><a id="A12" href="../Transactions/EditPurchaseOrder.aspx" runat="server" >Purchase Order Authorization </a></li>
                	<li><a id="A75" href="../Transactions/WorkOutJobOrder.aspx" runat="server" >Job Work Out  Order </a></li>
		<li><a id="A76" href="~/Transactions/servicepo.aspx" runat="server" > Service Purchase  </a></li>

				<li><a id="A61" href="~/Transactions/EditAuthPurchaseOrder.aspx" runat="server" >Purchase Order Editing</a></li>
			<li><a id="A10" href="~/Transactions/EmailPurchaseOrder.aspx" runat="server" >Purchase Order Follow Up</a></li>
				<li><%--<a id="A61" href="~/Transactions/EditAuthPurchaseOrderLogin.aspx" runat="server" >Purchase Order Editing</a>--%></li>
				<li><a id="A47" href="~/Account/PaymentVoucher.aspx" runat="server" >Payment Voucher</a></li>
				<li><a id="A13" href="~/Transactions/MaterialInwardRegister1.aspx" runat="server" >Inward Register For Material</a></li>
				<li><a id="A16" href="../Transactions/MaterialDamage1.aspx" runat="server" >Damage Register  For Material</a></li>
				<li><a id="A52" href="~/Transactions/MaterialReturn.aspx" runat="server" >Return Register  For Material</a></li>
				<li><a id="A14" href="~/Transactions/AssignStock.aspx" runat="server" >Issue Register  For Material</a></li>
				<li><a id="A53" href="~/Transactions/MaterialComsumption.aspx" runat="server" >Consumption Register  For Material</a></li>
				<li><a id="A17" href="~/Transactions/MatTransferLocation1.aspx" runat="server" >Transfer Register  For Material</a></li>
				<li><a id="A40" href="~/Transactions/CreateDeviation.aspx" runat="server" >Deviation Register  For Material</a></li>
			
				
			</ul>
		</li>
		<%--<li><a id="A03" href="#" runat="server">Reports</a>--%>
		<li><a href="#">Reports</a>
		<ul>
		  <li><a runat="server" id="b1">Material Indent</a>
		  <ul>
		  <li><a id="A26"  href="~/Reports/MaterialReqSummary.aspx" runat="server" >Summary</a></li>
		  <li><a id="A27"  href="~/Reports/MaterialReqDetails.aspx" runat="server" >Details</a></li>
		  <li><a id="A51"  href="~/Reports/PendingRequisitionReport.aspx" runat="server" >Status </a></li>
		  <li><a id="A63"  href="~/Reports/MaterialReqDetailsForPurchaseManager.aspx" runat="server" >Indent Generated</a></li>
		  </ul>
		  </li>
		  <li><a runat="server" id="b2">Purchase Order</a>
		  <ul>
		  <li><a id="A19"  href="~/Reports/POSummary.aspx" runat="server" >Summary</a></li>
		  <li><a id="A18"  href="~/Reports/PODetails.aspx" runat="server" >Details</a></li>
		  <li><a id="A59"  href="~/Reports/POStatus.aspx" runat="server" >Status</a></li>
		  <li><a id="A62"  href="~/Reports/POExcessQty.aspx" runat="server" >Excess Qty Report</a></li>
		  <li><a id="A64"  href="~/Reports/CancelPODetails.aspx" runat="server" >Cancel</a></li>
		  <li><a id="A999"  href="~/Reports/PO_Remaining.aspx" runat="server" >Remaining</a></li>
		  </ul>
		  </li>
		  

       <%--   <li><a id="A73" href="~/Reports/Enquiryreport.aspx" runat="server" >Enquiry Report </a></li>--%>
		  <li><a id="A54"  href="~/MISACCOUNT/SUNDRYCREDITOR.aspx" runat="server" >Outstanding Payment Sheet</a></li>
		  
		  <li><a runat="server" id="b3">Inward Register</a>
		  <ul>
		  <li><a id="A20"  href="~/Reports/MaterialInwardRegister.aspx" runat="server" >Summary</a></li>
		  <li><a id="A21"  href="~/Reports/InwardRegisterDetails.aspx" runat="server" >Details</a></li>
		  </ul>
		  </li>
		  <li><a runat="server" id="b4">Material Damage</a>
		  <ul>
		  <li><a id="A32"  href="~/Reports/MaterialDamageSummary.aspx" runat="server" >Summary</a></li>
		  <li><a id="A34"  href="~/Reports/MaterialDamageDetails.aspx" runat="server" >Details</a></li>
		  </ul>
		  </li>
		  <li><a runat="server" id="b5">Issue Register</a>
		  <ul>
		  <li><a id="A28"  href="~/Reports/AssignStockSummary.aspx" runat="server" >Summary</a></li>
		  <li><a id="A30"  href="~/Reports/AssignStockDetails.aspx" runat="server" >Details</a></li> 
		  <li><a id="A43"  href="~/Reports/AssignStockConsumption.aspx" runat="server" >Consumption</a></li> 
		  <li><a id="A46"  href="~/Reports/CostCentreSummary.aspx" runat="server" >Site Wise</a></li> 
		  </ul>
		  </li>
		  
		   <li><a runat="server" id="b6">Consumption Register</a>
		  <ul>
		  <li><a id="A55"  href="~/Reports/ConsumeStockSummary.aspx" runat="server" >Summary</a></li>
		  <li><a id="A56"  href="~/Reports/ConsumeStockDetails.aspx" runat="server" >Details</a></li> 
		  </ul>
		  </li>
		  
		
		  <li><a runat="server" id="b7">Material Transfer</a>

		  <ul>
		  <li><a id="A35"  href="~/Reports/MaterialTransferSummary.aspx" runat="server" >Summary</a></li>
		  <li><a id="A36"  href="~/Reports/MaterialTransferDetails.aspx" runat="server" >Details</a></li>
		  </ul>
		  </li>
		
		  <li><a runat="server" id="b8">Material Deviation</a>
		  <ul>
		  <li><a id="A23"  href="~/Reports/devationsummary.aspx" runat="server" >Details</a></li>
		  </ul>
		  </li>
		  
		  <li><a runat="server" id="b9">Inventory</a>
		  <ul>
		  <li><a id="A15"  href="#" runat="server">Stock</a>
		  <ul>
		  <li><a id="A58"  href="~/Reports/GroundStockReport.aspx" runat="server" >Stock Report</a></li>
		  <li><a id="A33"  href="~/Reports/StockReport.aspx" runat="server" >Stock Report with Details</a></li>		 
		  <li><a id="A22"  href="~/Reports/StockReportWithDeviation.aspx" runat="server" >Stock Report With Deviation</a></li>
		  </ul>
		  </li>
		 
		  <li><a id="A41"  href="~/Reports/MonthEndSummary.aspx" runat="server" >Monthly Report</a></li>
		  </ul>
		  </li>
		  <li><a runat="server" id="b10">Article History</a>
		  <ul>
		  <li><a id="A49"  href="~/Reports/ArticlesHistoryReport.aspx" runat="server" >Article History Inward</a></li>
		  <li><a id="A50"  href="~/Reports/ArticlesHistoryReportOutWard.aspx" runat="server" >Article History Outward</a></li>
		  </ul>
		  </li>
		   
		  
		</ul>
		</li>
		<%--<li><a id="A04" href="#" runat="server">Contact Us</a></li>--%>
		<li><a  href="#">Utilities</a>
		<ul>
		<li><a id="A889" href="~/ToImport.aspx" runat="server"> ToImport</a></li>
		<li><a id="A37"  href="~/Utility/SendeMail.aspx" runat="server" >Send E-Mail</a></li>
		<li><a id="A39"  href="~/Utility/BackupData.aspx" runat="server">BackUp Database</a></li>
		<li><a id="A48"  href="~/ChequePrint/ChequePrint.aspx" runat="server" >Cheque Printing</a></li>
		</ul>
		</li>
		<li><a  href="../ContactUs/ContactUs.aspx" runat="server">Contact Us</a></li>
			
	</ul>
</nav>
