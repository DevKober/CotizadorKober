(function() {
  var root;

  root = typeof exports !== "undefined" && exports !== null ? exports : this;

  root.display = function() {
    return alert('Funcion llamada desde coffescript');
  };

}).call(this);


