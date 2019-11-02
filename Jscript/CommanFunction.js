/**
*
*Created By Alok
* Date 6 Sept 2011
*
**/     
        
/** <!-- Make Popup for Installation & Commissioning-->    **/
        function Openpopup(popurl) {
            winpops = window.open(popurl, "", "width=670, height=570, left=120, top=45, scrollbars=yes, menubar=no,resizable=no,directories=no,location=no")
        }       

/** <!-- Make Popup for Installation & Commissioning-->    **/   
    
 
/**=====================Javascript For Print========================= **/                  
//  function CallPrint(strid)
//  {
//      var prtContent = document.getElementById(strid);
//      var WinPrint = window.open('','','letf=0,top=0,width=800,height=700,toolbar=0,scrollbars=1,status=0');
//      WinPrint.document.write(prtContent.innerHTML);
//      WinPrint.document.close();
//      WinPrint.focus();
//      WinPrint.print();
//      WinPrint.close();

//}
/**=====================Javascript For Print========================= **/  

/**=====================Javascript For Print========================= **/  
//function printdiv(printpage)
//{
//var headstr = "<html><head><title>Print Report</title></head><body>";
//var footstr = "</body>";
//var newstr = document.all.item(printpage).innerHTML;
//var oldstr = document.body.innerHTML;
//document.body.innerHTML = headstr+newstr+footstr;
//window.print();
//document.body.innerHTML = oldstr;
//return false;
//}               
/**=====================Javascript For Print========================= **/  

/**=====================Javascript For GridRowFoucs========================= **/
    var SelectedRow = null;
    var SelectedRowIndex = null;
    var UpperBound = null;
    var LowerBound = null;
    
    window.onload = function()
    {
        UpperBound = parseInt('<%= this.gridView.Rows.Count %>') - 1;
        LowerBound = 0;
        SelectedRowIndex = -1;        
    }
    
    function SelectRow(CurrentRow, RowIndex)
    {        
        if(SelectedRow == CurrentRow || RowIndex > UpperBound || RowIndex < LowerBound) return;
         
        if(SelectedRow != null)
        {
            SelectedRow.style.backgroundColor = SelectedRow.originalBackgroundColor;
            SelectedRow.style.color = SelectedRow.originalForeColor;
        }
        
        if(CurrentRow != null)
        {
            CurrentRow.originalBackgroundColor = CurrentRow.style.backgroundColor;
            CurrentRow.originalForeColor = CurrentRow.style.color;
            CurrentRow.style.backgroundColor = '#B6B6B6';
            CurrentRow.style.color = 'Black';
        } 
        
        SelectedRow = CurrentRow;
        SelectedRowIndex = RowIndex;
        setTimeout("SelectedRow.focus();",0); 
    }
    
    function SelectSibling(e)
    { 
        var e = e ? e : window.event;
        var KeyCode = e.which ? e.which : e.keyCode;
        
        if(KeyCode == 40)
            SelectRow(SelectedRow.nextSibling, SelectedRowIndex + 1);
        else if(KeyCode == 38)
            SelectRow(SelectedRow.previousSibling, SelectedRowIndex - 1);
            
        return false;
    }
    /**=====================Javascript For GridRowFoucs========================= **/
    
   /**=====================Javascript For Multilne TextBox========================= **/
/*=========== Write On TextBox Event onKeyUp="javascript:Count(this,200);" onChange="javascript:Count(this,200);" ===========*/
//    function MultilineMaxLength(text,long)

//    {

//    var maxlength = new Number(long); // Change number to your max length.

//    if(document.getElementById('<%=textBox.ClientID%>').value.length > maxlength){

//    text.value = text.value.substring(0,maxlength);

//    alert(" Only " + long + " chars");

//    }    
/**=====================Javascript For Multilne TextBox========================= **/





