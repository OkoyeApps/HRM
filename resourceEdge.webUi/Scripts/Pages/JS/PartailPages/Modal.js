/// <reference path="Modal.js" />
var Code = new jBox('Modal', {
    id : "codeModal",
    content:$('#codeContent'),
    title: '<h4>Add Identity Code</h4>',
    width:900,
    height: 350,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var career = new jBox('Modal', {
    //attach: "#career",
    id: "jModal",
    content: $('#careerContent'),
    title: '<h4>Add Career </h4>',
    width: 700,
    height: 250,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var stat = new jBox('Modal', {
    //attach: "#status",
    id: "statusModal",
    content: $('#statusContent'),
    title:'<h4>Add employment status </h4>',
    width: 500,
    height: 250,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var group1 = new jBox('Modal', {
    attach: "#group",
    id: "jModal",
    content: $('#groupContent'),
    title: ' <h4>Add Group </h4>',
    width: 900,
    height: 250,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var job = new jBox('Modal', {
    attach: "#job",
    id: "jModal",
    content: $('#jobContent'),
    title: '<h4>Add Job</h4>',
    width: 900,
    height: 450,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var level = new jBox('Modal', {
    attach: "#level",
    id: "jModal",
    content: $('#levelContent'),
    title: '<h4>Add Level </h4>',
    width: 900,
    height: 300,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var locmodal = new jBox('Modal', {
    attach: "#location",
    id: "jModal",
    content: $('#locationContent'),
    title: '<h4>Add Location </h4>',
    width: 900,
    height: 390,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});

var position = new jBox('Modal', {
    attach: "#position",
    id: "jModal",
    content: $('#positionContent'),
    title:'<h4>Add Position</h4>',
    width: 900,
    height: 300,
    blockScroll: false,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
});


var prefixes = new jBox('Modal', {
    content: $('#prefixContent'),
    id: "jModal",
    title: '<h4>Add Prefix</h4>',
    width: 600,
    height: 300,
    blockScroll: true,
    animation: 'zoomIn',
    draggable: 'title',
    closeButton: true,
    overlay: false,
    reposition: false,
    repositionOnOpen: false
})

