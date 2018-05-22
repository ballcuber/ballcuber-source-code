/*
 * twisty_demo.js
 * 
 * Demonstration and testing harness for WSOH.
 * 
 * TOOD
 * - Fix document.getElementById(...) calls.
        // TODO I can imagine that some users of twisty.js would want to be able to have a Heise-style
        // inspection, where you are only allowed to do inspection moves during inspection, rather than
        // just starting the timer when they do a turn. This will require somehow being able to cancel/prevent a move?
        // TODO clicking on canvas doesn't seem to focus window in firefox
        // TODO clicking and dragging is weird when the mouse leaves the window
        // TODO keydown doesn't repeat on firefox
 * 
 */

"use strict";

var cache = window.applicationCache;
function updateReadyCache() {
  window.applicationCache.swapCache();
  location.reload(true); // For now
}

var startTime = null;
var stopTime = null;
function startTimer() {
  startTime = new Date().getTime();
  stopTime = null;
  refreshTimer();
  startRefreshTimerLoop();
}
function isTiming() {
  return startTime != null && stopTime == null;
}
function stopTimer() {
  assert(startTime);
  stopTime = new Date().getTime();
  refreshTimer();
  stopRefreshTimerLoop();
}

function resetTimer() {
  startTime = null;
  stopTime = null;
  refreshTimer();
  stopRefreshTimerLoop();
}

function refreshTimer() {
  var timer = $("#timer");
  timer.removeClass("reset running stopped");
  if(isTiming()) {
    timer.addClass("running");
    timer.text(prettyTime(new Date().getTime()));
  } else if(startTime == null) {
    assert(stopTime == null);
    timer.addClass("reset");
    timer.text("[Timer]");
  } else if(stopTime != null) {
    assert(startTime);
    timer.addClass("stopped");
    timer.text(prettyTime(stopTime));
  }
}

var pendingTimerRefresh = null;
function startRefreshTimerLoop() {
  if(pendingTimerRefresh == null) {
    pendingTimerRefresh = requestAnimationFrame(refreshTimerLoop, $('#timer')[0]);
  }
}
function stopRefreshTimerLoop() {
  if(pendingTimerRefresh != null) {
    cancelRequestAnimationFrame(pendingTimerRefresh);
    pendingTimerRefresh = null;
  }
}
function refreshTimerLoop() {
  refreshTimer();
  if(pendingTimerRefresh != null) {
    pendingTimerRefresh = requestAnimationFrame(refreshTimerLoop, $('#timer')[0]);
  }
}

function pad(n, minLength) {
  var str = '' + n;
  while (str.length < minLength) {
    str = '0' + str;
  }
  return str;
}

function prettyTime(endTime) {
  var cumulative = endTime - startTime;
  var str = "";
  str += Math.floor(cumulative/1000/60);
  str += ":";
  str += pad(Math.floor(cumulative/1000 % 60), 2);
  str += ".";
  str += pad(Math.floor((cumulative % 1000) / 10), 2);
  return str;
}


var CubeState = {
  solved: 0,
  scrambling: 1,
  scrambled: 2,
  solving: 3,
};
var cubeState = null;

var twistyScene;

var stage = [
		[1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1],
		[2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2],
		[3,3,3,3,3,3,3,3,3,3,3,3,3,3,3,3],
		[4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4],
		[5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5],
		[6,6,6,6,6,6,6,6,6,6,6,6,6,6,6,6]
];


function setColors(colors){
	stage = colors;
	reloadCube();
}

function move(pBase, pStartLayer, pEndLayer, pAmount){
	twistyScene.queueMoves([{base: pBase, startLayer: pStartLayer, endLayer: pEndLayer, amount: pAmount}]);
    twistyScene.play.start();
}

  function reloadCube() {
    log("reload cube");
	
	// store camera position
	if(twistyScene != undefined){
		var formerTheta = twistyScene.getCameraTheta();
		var formerHeight = twistyScene.getCameraHeight();
	}
	
    var renderer = THREE["WebGLRenderer"];
	

    twistyScene = new twisty.scene({
      renderer: renderer,
      allowDragging: true,
      "speed": 4,
      stats: true
    });
	
    $("#twistyContainer").empty();
    $("#twistyContainer").append($(twistyScene.getDomElement()));

    twistyScene.initializePuzzle({
      "type": "cube",
      "dimension": 4,
      "stage": stage,
      "doubleSided": false,
      "cubies": true,
      "hintStickers": true,
      "stickerBorder": false
    });
    twistyScene.resize();
    cubeState = CubeState.solved;
    resetTimer();

    twistyScene.addListener("moveStart", function(move) {
      if(cubeState == CubeState.scrambling) {
        // We don't want to start the timer if we're scrambling the cube.
      } else if(cubeState == CubeState.scrambled) {
        if(twistyScene.debug.model.twisty.isInspectionLegalMove(move)) {
          return;
        }
        startTimer();
        cubeState = CubeState.solving;
      }
    });

    twistyScene.addListener("moveAdvance", function(move) {
      if(cubeState == CubeState.solving && twistyScene.debug.model.twisty.isSolved()) {
        cubeState = CubeState.solved;
        stopTimer();
      }
    });
	
	// set previous camera position
	twistyScene.setCameraPosition(formerTheta || 0, formerHeight);
  }
  

$(document).ready(function() {

  var webgl = ( function () { try { var canvas = document.createElement( 'canvas' ); return !! window.WebGLRenderingContext && ( canvas.getContext( 'webgl' ) || canvas.getContext( 'experimental-webgl' ) ); } catch( e ) { return false; } } )();

  /*
   * Caching Stuff.
   */
  cache.addEventListener('updateready', updateReadyCache, false);

  log("Document ready.");

  reloadCube();

  $("#alg_ccc").bind("click", function() {
    twistyScene.queueMoves(makeCCC(4));
    twistyScene.play.start();
  });

  $("#reload").bind("click", function() {
    stage[0][6] = 2;
	reloadCube();
  });

  $("#move1").bind("click", function() {
    move("u", 1, 2, -1);
  });

  $("#move2").bind("click", function() {
    move("l", 2, 3, 1);
  });

  $("#move3").bind("click", function() {
    move("u", 1, 4, 1);
  });

  $(window).resize(twistyScene.resize);

  // TODO add visual indicator of cube focus --jfly
  // clear up canvasFocused stuff...
  //$("#twistyContainer").addClass("canvasFocused");
  //$("#twistyContainer").removeClass("canvasFocused");

  $(window).keydown(function(e) {
    // This is kinda weird, we want to avoid turning the cube
    // if we're in a textarea, or input field.
    var focusedEl = document.activeElement.nodeName.toLowerCase();
    var isEditing = focusedEl == 'textarea' || focusedEl == 'input';
    if(isEditing) {
      return;
    }

    var keyCode = e.keyCode;
    switch(keyCode) {
      case 27:
        reloadCube();
        e.preventDefault();
        break;

      case 32:
        if (!isTiming()) {
          var twisty = twistyScene.debug.model.twisty;
          var scramble = twisty.generateScramble(twisty);
          // We're going to get notified of the scrambling, and we don't
          // want to start the timer when that's happening, so we keep track
          // of the fact that we're scrambling.
          cubeState = CubeState.scrambling;
          twistyScene.applyMoves(scramble); //TODO: Use appropriate function.
          twistyScene.redraw(); // Force redraw.
          cubeState = CubeState.scrambled;
          resetTimer();
        }
        e.preventDefault();
        break;
    }

    twistyScene.keydown(e);
  });

});

/*
 * Convenience Logging
 */

var logCounter = 0;

function log(obj) {
  if(typeof(console) !== "undefined" && console.log) {
    console.log(obj);
  }
}

function err(obj) {
  if(typeof(console) !== "undefined" && console.error) {
    console.error(obj);
  }
}

/*
 * Algs for testing
 */

function makeCCC(n) {

  var cccMoves = [];

  for (var i = 1; i<=n/2; i++) {
    var moreMoves = [
      {base: "l", endLayer: i, amount: -1},
      {base: "u", endLayer: i, amount: 1},
      {base: "r", endLayer: i, amount: -1},
      {base: "f", endLayer: i, amount: -1},
      {base: "u", endLayer: i, amount: 1},
      {base: "l", endLayer: i, amount: -2},
      {base: "u", endLayer: i, amount: -2},
      {base: "l", endLayer: i, amount: -1},
      {base: "u", endLayer: i, amount: -1},
      {base: "l", endLayer: i, amount: 1},
      {base: "u", endLayer: i, amount: -2},
      {base: "d", endLayer: i, amount: 1},
      {base: "r", endLayer: i, amount: -1},
      {base: "d", endLayer: i, amount: -1},
      {base: "f", endLayer: i, amount: 2},
      {base: "r", endLayer: i, amount: 2},
      {base: "u", endLayer: i, amount: -1}
    ];

    cccMoves = cccMoves.concat(moreMoves);
  }

  return cccMoves;

}
