document.addEventListener('DOMContentLoaded', function() {
  var elems_down = document.querySelectorAll('.dropdown-trigger');
  var instances_down = M.Dropdown.init(elems_down, options);
  });

$(document).ready(function(){
  $('.dropdown-trigger').dropdown();
});