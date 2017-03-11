// Write your Javascript code.
function copiedSecretToClipboard() {
    $("span#copiedSecret").delay(150).removeClass("badge-danger").addClass("badge-info").fadeIn(450);
    $("span#copiedLink").removeClass("badge-info").addClass("badge-danger").fadeOut(150);
}
function copiedLinkToClipboard() {
    $("span#copiedSecret").removeClass("badge-info").addClass("badge-danger").fadeOut(150);
    $("span#copiedLink").delay(150).removeClass("badge-danger").addClass("badge-info").fadeIn(450);
    
}
