import { useParams } from 'react-router-dom';
import { gameRepository } from '../../../repositories';
import { useEffect, useState } from 'react';
import GameModel from '../../../models/GameModel';
import { BASE_API_URL } from '../../../repositories/apiConstatns';

function GameDetails() {
    const { id } = useParams();
    const { get } = gameRepository;
    const [game, setGame] = useState<GameModel>();

    useEffect(() => {
        if (id) {
            get(+id).then((response) => {
                setGame(response.data as GameModel);
            });
        } else {
            console.error('There no ID');
        }
    }, []);

    return (
        <div>
            {!game && <div>loading</div>}

            {!!game && (
                <div>
                    <img
                        src={`${BASE_API_URL}images/Game/cover-${game.id}.jpg`}
                        alt="cover"
                    />
                    game name: {game.name} ({game.id})
                </div>
            )}
        </div>
    );
}

export default GameDetails;
