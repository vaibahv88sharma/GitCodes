#Add-PSSnapin "Microsoft.SharePoint.PowerShell"

### GLOBAL VARS, CHANGE HERE
$listname = "Style Library" 
$url = "http://staffportal.myselfserve.com.au/sites/portal/"
 
function approveContent ($w, $listName) {
  $list = $w.Lists |? {$_.Title -eq $listName}
  foreach ($item in $list.Items) 
  {
    if(($item -ne $null) -and ($item.LockId -ne $null)) {
      $item.ReleaseLock($item.LockId)
    }
    if( $item.File -ne $null) { $itemFile = $list.GetItemById($item.ID).File }
    else { $itemFile = $list.GetItemById($item.ID) }
    
    if( $itemFile.CheckOutStatus -ne "None" ) { 
      $itemFile.CheckIn("Automatic CheckIn. (Administrator)")
      if( $item.File -ne $null) { $itemFile = $list.GetItemById($item.ID).File }
      else { $itemFile = $list.GetItemById($item.ID) }
    }
    if( $list.EnableVersioning -and $list.EnableMinorVersions) { 
      $itemFile.Publish("Automatic Publish. (Administrator)")
      if( $item.File -ne $null) { $itemFile = $list.GetItemById($item.ID).File }
      else { $itemFile = $list.GetItemById($item.ID) }
    }
    if( $list.EnableModeration ) { 
      $itemFile.Approve("Automatic Approve. (Administrator)") 
    }
  }
}
 
$site = Get-SPSite $url
foreach ( $web in $site.AllWebs )
{
 approveContent $web $listname
}
Write-Output "OK"