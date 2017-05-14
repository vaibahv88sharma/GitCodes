<?php

    $to="info@ataccounting.com.au";

    $from=$HTTP_POST_VARS['email'];
//	$headers  = "MIME-Version: 1.0\r\n";
	$headers .= "Content-type: text/html; charset=iso-8859-1\n";
	$headers .= "From: ".$HTTP_POST_VARS['email']."\n";
	
$mailbody='
<html>
<head>
<title>Aerry Tax & Accounting</title>
</head>
<body bgcolor="#FFFFFF" >
<table border="0" cellpadding="0"
  cellspacing="0" width="552">
    <tr align="center">
      <td align="center" width="1089"><em><strong><small><font color="#FF0000">Customer
                Query from Aerry Tax & Accounting</font></small></strong></em></td>
    </tr>
    <tr align="center">
      <td width="550">&nbsp;&nbsp; </td>
    </tr>
</table>
  <table BORDER="0" WIDTH="552" cellspacing="0"
  cellpadding="0">
    <tr>
      <td width="302" height="25"><font FACE="arial" size="2" color="#000000"><strong>Your Name</strong></font></td>
      <td width="303" height="25"><font color="#000000">      '.$HTTP_POST_VARS["name"].'</font></td>
    </tr>
	 <tr>
      <td width="302" height="25"><font FACE="arial" size="2" color="#000000"><strong>Your Email</strong></font></td>
      <td width="303" height="25"><font color="#000000">      '.$HTTP_POST_VARS["email"].'</font></td>
    </tr>
    <tr>	
      <td width="302" height="25"><font FACE="arial" size="2" color="#000000"><strong>Subject</strong></font></td>
      <td width="303" height="25"><font color="#000000">      '.$HTTP_POST_VARS["subject"].'</font></td>
    </tr>
	    <tr>
      <td width="302" height="26" valign="top"><font FACE="ARIAL" size="2" color="#000000"><strong>Message</strong></font></td>
      <td width="303" height="26"><font color="#000000">'.$HTTP_POST_VARS["requiredmessage"].'</font></td>
    </tr>

  </table>
</form>
</body>
</html>
';


//print $mailbody;

    @mail("$to","User query from Aerry Tax & Accounting Website","$mailbody","$headers");
    $response='<html>
<body>
<table width="90%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td colspan="2" bgcolor="#FE9649"><strong><font color="#FFFFFF" size="2" face="Arial, Helvetica, sans-serif">Thank you for contacting us</font></strong></td>
  </tr>
  <tr valign="middle">
    <td height="63" colspan="2" bgcolor="#F8F8F8"><div align="center"><FONT face=Georgia, Times New Roman, Times, serif size=2>This is an autoresponse to let you know that we have received your mail. A member of our staff will be contacting you soon.</FONT></div></td>
  </tr>
  <tr>
    <td width="71%">&nbsp;</td>
    <td width="29%"><div align="right"><strong><font color="#343350" size="2" face="Arial, Helvetica, sans-serif"></font></strong></div></td>
  </tr>
</table>
</body>
</html>
';

    @mail($HTTP_POST_VARS['email'],"Thank you for contacting V.K Concrete","$response","$headers");
    @header("Location: thanks.html");
    exit();
?>
