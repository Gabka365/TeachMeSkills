import React from 'react';
import styles from './traveling.module.css';
import TravelingModel from '../../../../models/TravelingModel';
import travelingRepsoitory from '../../../../repositories/travelingRepsoitory';
import { useState, FC, useCallback } from 'react';
import { BASE_API_URL } from '../../../../repositories/apiConstatns';


interface TravelingProp {
    traveling: TravelingModel;
    onDelete: (id: number) => void;
}


const Traveling: FC<TravelingProp> = ({ traveling, onDelete }) => {
    const { remove } = travelingRepsoitory;
    const [isDeleted, setIsDeleted] = useState(false);
    const removeTraveling = useCallback((id: number) => {
        setIsDeleted(true);
        remove(id);
        onDelete(id);
    }, []);

    return (
        <div className={styles.wrapper}>
            <img
                src={`${BASE_API_URL}images/Traveling/UserPictures/${traveling.id}.png`}
                alt="cover"
            />
            <h2 className={styles.name}>{traveling.name}</h2>
            <p className={styles.desc}>{traveling.desc}</p>
            <button onClick={() => removeTraveling(traveling.id)}>Удалить</button>
        </div>
    );
};

export default Traveling;
