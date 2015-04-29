<html>
    <head>
        <title>Gues Number Game</title>
    </head>
    <body>
        <form class="" action="play.php" method="post">
            <input type="text" name="user" value="" placeholder="Enter your name">
            <input type="submit" name="name" value="Start Game">
        </form>
            <?php
                session_start();
                $secretNumber = rand(1, 100);
                if (!isset($_SESSION['secretNumber'])) {
                    $_SESSION['secretNumber'] = $secretNumber;
                }
             ?>
    </body>
</html>
