Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

function beginReq(sender, args) {
    // shows the Popup
    divLoad.className = "CssLoad"

}

function endReq(sender, args) {
    //  shows the Popup
    divLoad.className = "none"

} 
