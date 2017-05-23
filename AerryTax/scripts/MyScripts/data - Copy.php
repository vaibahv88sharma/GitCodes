 <?php
 
    //<add name="Access-Control-Allow-Origin" value="*" />
    //<add name="Access-Control-Allow-Headers" value="Content-Type" />
    //<add name="Access-Control-Allow-Methods" value="GET, POST, PUT, DELETE, OPTIONS" />
	
    header("Access-Control-Allow-Methods: POST, GET, OPTIONS");
    header("Access-Control-Allow-Origin: *");
    header("Access-Control-Allow-Headers: Content-Type");
    header('Access-Control-Max-Age: 86400');
	echo "invalid";
	
    $postData       = isset($_REQUEST)  ? $_REQUEST                 : null; 
    $jsonEncoded    = ($postData)       ? json_encode($postData)    : null;
    $response       = array();
    $callback       = null;

    if ($jsonEncoded) {
        $htmlContent    = json_decode($jsonEncoded);
        $fName          = htmlspecialchars(trim($htmlContent->fName));
        $lName          = htmlspecialchars(trim($htmlContent->lName));
        $email          = htmlspecialchars(trim($htmlContent->email));
        $phone          = htmlspecialchars(trim($htmlContent->phone));
        //$callback       = htmlspecialchars(trim($htmlContent->callback));

        if ($fName != "") {
            $response['message']    = "Server returns: " . $fName;
        }else {
            $response['message']    = "Empty username parameter!";
        }
    }else {
        $response['message']        = "Not called properly with username parameter!";
    }

    <!--if($callback) {
        die($callback . "(" . json_encode($response) . ")");
    }else{
        die(json_encode($response));
    }-->	
	
  
$to      = 'vaibhav88sharma@gmail.com';
$subject = 'the subject';
$message = 'hello';
$headers = 'From: webmaster@example.com' . "\r\n" .
    'Reply-To: webmaster@example.com' . "\r\n" .
    'X-Mailer: PHP/' . phpversion();

mail($to, $subject, $message, $headers);  
  
	//$htmlContent = json_decode($postdata);
	
    // Allow from any origin
    //if (isset($_SERVER['HTTP_ORIGIN'])) {
    //    header("Access-Control-Allow-Origin: *");
	//	//header("Access-Control-Allow-Origin: {$_SERVER['HTTP_ORIGIN']}");
    //    header('Access-Control-Allow-Credentials: true');
    //    header('Access-Control-Max-Age: 86400');    // cache for 1 day
	//	//header('Access-Control-Allow-Headers: Content-Type');
    //}
    //// Access-Control headers are received during OPTIONS requests
    //if ($_SERVER['REQUEST_METHOD'] == 'OPTIONS') {
    //
    //    if (isset($_SERVER['HTTP_ACCESS_CONTROL_REQUEST_METHOD']))
    //        header("Access-Control-Allow-Methods: GET, POST, OPTIONS");         
    //
    //    if (isset($_SERVER['HTTP_ACCESS_CONTROL_REQUEST_HEADERS']))
    //        header("Access-Control-Allow-Headers:        {$_SERVER['HTTP_ACCESS_CONTROL_REQUEST_HEADERS']}");
    //
    //    exit(0);
    //}

	
//	$to = 'vaibhav88sharma@gmail.com';
//	$subject = "Beautiful HTML Email using PHP by CodexWorld";
//// Get HTML contents from file
//
//    $postdata = file_get_contents("php://input");
//	
//	if (isset($postdata)) {
//		
//		$htmlContent = json_decode($postdata);
//
//		$fName  =   $htmlContent->fName;
//		$lName  =   $htmlContent->lName;
//		$email  =   $htmlContent->email;
//		$phone  =   $htmlContent->phone;	
//	
//	  /*
//	  @$message  =   $htmlContent->message;
//	  */
//
//		if ($fName != "") {
//			echo "Server returns: " . $fName;
//		}
//		else {
//			echo "Empty username parameter!";
//		}
//		
//		echo $data->name;
//		// Set content-type for sending HTML email
//		$headers = "MIME-Version: 1.0" . "\r\n";
//		$headers .= "Content-type:text/html;charset=UTF-8" . "\r\n";
//
//		// Additional headers
//		$headers .= 'From: vaibhav88sharma@gmail.com' . "\r\n";
//		$headers .= 'Cc: vaibhav88sharma@gmail.com' . "\r\n";
//
//		// Send email
//		if(mail($to,$subject,$htmlContent,$headers)):
//			$successMsg = 'Email has sent successfully.';
//		else:
//			$errorMsg = 'Some problem occurred, please try again.';
//		endif;		
//		
//	}
//	else {
//		echo "Not called properly with username parameter!";
//	}	
	


?>
