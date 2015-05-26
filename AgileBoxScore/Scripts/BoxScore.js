// A simple templating method for replacing placeholders enclosed in curly braces.
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

$(function () {

    var ticker = $.connection.gameBox, // the generated client-side hub proxy
        up = '▲',
        down = '▼',
        $boxScoreTable,
        $boxScoreTableBody,
        awayRowTemplate = '<tr data-team="away"><td>{AwayTeam}</td><td>{AwayRuns}</td><td>{AwayHits}</td><td>{AwayError}</td></tr>',
        homeRowTemplate = '<tr data-team="home"><td>{HomeTeam}</td><td>{HomeRuns}</td><td>{HomeHits}</td><td>{HomeError}</td></tr>',
        infoRowTemplate = '<tr data-team="info"><td colspan="2">Inning - {Inning} {Top}</td><td>Outs - {Outs}</td></tr>';

    function init() {
        ticker.server.getAllGames().done(function (games) {
            $.each(games, function () {
                var game = this;

                if (game.IsTopOfInning) {
                    game.Top = up;
                } else {
                    game.Top = down;
                }
                $boxScoreTable = $('#' + game.Id);
                $boxScoreTableBody = $boxScoreTable.find('tbody');
                $boxScoreTableBody.append(awayRowTemplate.supplant(game));
                $boxScoreTableBody.append(homeRowTemplate.supplant(game));
                $boxScoreTableBody.append(infoRowTemplate.supplant(game));
                
            });
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updateGameScore = function (game) {
        if (game.IsTopOfInning) {
            game.Top = up;
        } else {
            game.Top = down;
        }

        var $homeRow = $(homeRowTemplate.supplant(game)),
            $awayRow = $(awayRowTemplate.supplant(game)),
            $infoRow = $(infoRowTemplate.supplant(game));

        $boxScoreTable = $('#' + game.Id);
        $boxScoreTable.toggleClass('shade');

        $boxScoreTableBody = $boxScoreTable.find('tbody');

        $boxScoreTableBody.find('tr[data-team="away"]')
            .replaceWith($awayRow);
        $boxScoreTableBody.find('tr[data-team="home"]')
            .replaceWith($homeRow);
        $boxScoreTableBody.find('tr[data-team="info"]')
            .replaceWith($infoRow);
    }

    // Start the connection
    $.connection.hub.start().done(init);

});