$(document).ready(function(){
    ValuedLoop(700);
})

function ValuedLoop(value) {
    for (i = 0; i < value - 1; i++) {
        CloneThis();        
    }
}

function CloneThis() {
    // get the last DIV which ID starts with ^= "container"
    var $div = $('div[id^="container"]:last');

    // Read the Number from that DIV's ID
    // And increment that number by 1
    var num = parseInt($div.prop("id").match(/\d+/g), 10) + 1;

    var incrementedID = 'container' + num;

    // Clone it and assign the new ID
    var $container = $div.clone(true).prop('id', incrementedID);

    // Finally insert $container wherever you want
    $div.after($container);
}

