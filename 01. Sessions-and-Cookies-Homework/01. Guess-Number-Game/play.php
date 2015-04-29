<html>
    <head>
        <title>Play</title>
    </head>
    <body>
        <form class="" action="" method="post">
            <label for="">Enter number in range [1..100]</label>
            <input type="text" name="guessNumber" value="">
            <input type="submit" name="submit" value="Submit">
        </form>
        <form class="" action="index.php" method="post" id="play-again" style="display:none">
            <input type="submit" name="name" value="Play Again">
        </form>
            <?php
            session_start();
            if (!isset($_SESSION["user"])) {
                $_SESSION["user"] = $_POST["user"];
            }

            $minNumber = 1;
            $maxNumber = 100;
            $user = $_SESSION['user'];
            if (isset($_SESSION['secretNumber'])) {
                $secretNumber = $_SESSION['secretNumber'];
            }

            if (isset($_POST['submit'])) {
                if (isset($_POST['guessNumber'])) {
                    $guessNumber = $_POST['guessNumber'];
                    echo $guessNumber;
                    if (($guessNumber < $minNumber || $guessNumber > $maxNumber) ) {
                        echo " Invalid Number";
                    }
                    elseif ($guessNumber == $secretNumber) {
                        echo " Congratulations, ". $user . "!";
                        session_destroy();
                        echo '<script type="text/javascript">',
                               'var form = document.getElementById("play-again");',
                               'form.style.display = "block";',
                        '</script>';
                    }

                    elseif ($guessNumber < $secretNumber) {
                        echo  " UP";
                    }
                    elseif ($guessNumber > $secretNumber) {
                        echo " DOWN";
                    }

                }
            }
             ?>
    </body>
</html>
