<?php
session_start();
$name = htmlentities(stripslashes($_POST['name']));
$email = htmlentities(stripslashes($_POST['email']));

$check_number = htmlentities(stripslashes($_REQUEST['check_number']));
    $number = htmlentities(stripslashes($_REQUEST['number']));
	
	$check_number  = "1";
	
	$number = '1';
	
		   if($number!=$check_number)
		   {
$check_number=="";
$number=="";
					header('Location: enquiry.php?error1=true');
				}
				else
				{
	
$date = date("j F, Y"); 


$subject1="Enquiry from Aerry Tax & Accounting";

$to = "amit@rtpltech.com";

$header1= "From:".htmlentities(stripslashes($_POST['email']))."\n";

$header1.="Content-Type: text/html; charset=iso-8859-1\n";

$message1="
<html>
<head>
<style type='text/css'>
<!--
.style3 {
	text-decoration: none;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 11px;
	color:#666666;
}
-->
</style>
</head>
<body>
<table width='460' border='0' align='left' cellpadding='0' cellspacing='0' style='border:#EFEFEF 5px solid; padding:5px;' >
  <tr style='background-color:#EEEEEE;'>
    <td height='70' align='left'>
    <table width='100%'><tr><td align='left'>
    <img border='0' src='http://saharahomes.com.au/images/sahara_logo.jpg' />
    </td><td align='right' class='style3' valign='bottom'>".$date."</td>
    </tr></table>
    </td>
  </tr>
  <tr>
    <td align='left' valign='top'><table width='99%' border='0' align='center' cellpadding='3' cellspacing='0'>
        <tr>
          <td align='left' valign='top' class='style3'>Enquiry from Sahara Homes. Details are given below:- </td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'>&nbsp;</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><strong>Name :</strong> ".htmlentities(stripslashes($_POST['name']))."</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><strong>Name of Organization :</strong> ".htmlentities(stripslashes($_POST['organization']))."</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><strong>City :</strong> ".htmlentities(stripslashes($_POST['city']))." </td>
        </tr>
		<tr>
          <td align='left' valign='top' class='style3'><strong> Country :</strong> ".htmlentities(stripslashes($_POST['country']))." </td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><strong> Contact Number :</strong> ".htmlentities(stripslashes($_POST['contact']))." </td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><strong>Email :</strong> ".htmlentities(stripslashes($_POST['email']))."</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><strong>Comments :</strong> ".htmlentities(stripslashes($_POST['comments']))." </td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'>&nbsp;</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'>Please keep this email for future reference.</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'>&nbsp;</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'><br>
            <strong>Thanks &amp; Regards</strong><br />
            www.saharahomes.com.au</td>
        </tr>
        <tr>
          <td align='left' valign='top' class='style3'>&nbsp;</td>
        </tr>
    </table></td>
  </tr>
</table>
</body>
</html>
";

if(($name!='')&&($email!='')&&($email!='hacker@hacker.org'))
{
mail($to,$subject1,$message1,$header1);
}
			  
			  print("<script>window.location='thanks.html';</script>");
				}
?>
