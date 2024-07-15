import { useEffect, useState, useCallback } from 'react';
import { Traveling, LastNews } from './Index';
import travelingRepsoitory from '../../../repositories/travelingRepsoitory';
import TravelingModel from '../../../models/TravelingModel';
import { Link } from 'react-router-dom';

function Travelings() {
    const { getAll } = travelingRepsoitory;
    const [travelings, setTravelings] = useState<TravelingModel[]>([]);

    useEffect(() => {
        getAll().then((response) => {
            setTravelings(response.data);
        });
    }, []);

    const onTravelingDelete = useCallback((id: number) => {
        setTravelings((oldTravelings) => [
            ...oldTravelings.filter((t) => t.id !== id),
        ]);
    }, []);
    return (
        <div className="Travelings">
            <LastNews />
            {travelings.length > 0 ? (
                travelings.map((traveling) => (
                    <div key={traveling.id}>
                        <Traveling
                            traveling={traveling}
                            onDelete={onTravelingDelete}
                        />
                    </div>
                ))
            ) : (
                <p>Ничего не найдено или сервер отключен</p>
            )}
            <Link to={'/traveling/create'}>Создать</Link>
        </div>
    );
}
export default Travelings;