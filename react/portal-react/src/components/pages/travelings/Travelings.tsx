import { useEffect, useState, useCallback } from 'react';
import { Traveling, LastNews } from './Index';
import travelingRepsoitory from '../../../repositories/travelingRepsoitory';
import TravelingModel from '../../../models/TravelingModel';

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
            ...oldTravelings.filter((g) => g.id !== id),
        ]);
    }, []);
    return (
        <div className="Travelings">
            <LastNews></LastNews>
            {travelings.map((traveling) => (
                <div>
                    <Traveling
                        traveling={traveling}
                        onDelete={onTravelingDelete}
                        key={traveling.id}
                    />
                </div>
            ))}
        </div>
    );
}
export default Travelings;