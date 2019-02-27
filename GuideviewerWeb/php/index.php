<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <title>Comprehensive Guide To All Cape Requirements</title>
  <link rel="stylesheet" href="../css/index.css">
  <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8=" crossorigin="anonymous"></script> 
  <script src="../js/index.js"></script> 
</head>

<body>
  <div id="container1">
    <div id="A">
      <p class="informationHolder">[1]</p>
    </div>
    <div id="L">
      <p class="informationHolder">[1]</p>
    </div>
    <div id="M">
      <p class="informationHolder">[1]</p>
    </div>
    <div id="C">
      <p class="informationHolder">[1]</p>
    </div>
    <div id="T">
      <p class="informationHolder">[1]</p>
    </div>
    <div id="I">
      <p class="informationHolder">[1]</p>
    </div>
  </div>

  <script>
    $(".informationHolder").click(function(){
      console.log(this.innerHTML);
      $(this).parent().hide();

      this.textContent = "";
    });
  </script>
</body>
</html>