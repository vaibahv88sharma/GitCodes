<?php
    header("Access-Control-Allow-Methods: POST, GET, OPTIONS");
    header("Access-Control-Allow-Origin: *");
    header("Access-Control-Allow-Headers: Content-Type");
    header('Access-Control-Max-Age: 86400');

/**
 * @filesource : submit.php
 * @author : Shabeeb  <me@shabeebk.com>
 * @abstract : simple submission php form
 * @package sample file 
 * @copyright (c) 2014, Shabeeb
 * shabeebk.com
 * 
 *  */

$post_date = file_get_contents("php://input");
$data = json_decode($post_date);


//saving to database
//save query

//now i am just printing the values
echo "Name : ".$data->name."\n";
echo "Email : ".$data->email."\n";
echo "Message : ".$data->message."\n";


echo "Hello world";

?>
