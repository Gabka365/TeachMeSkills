import React from 'react';
import styles from './lastNews.module.css';
import travelingRepsoitory from '../../../../repositories/travelingRepsoitory';
import newsModel from '../../../../models/NewsModel';
import { useEffect, useState } from 'react';

function LastNews() {
    const { getLastNews } = travelingRepsoitory;

    const [lastNews, setLastNews] = useState<newsModel | undefined>(undefined);

    useEffect(() => {
        getLastNews().then((response) => {
            setLastNews(response.data);
        });
    }, []);

    return (
        <div className={styles.wrapper}>
            <h2 className={styles.title}>Эта последняя новость за сегодня</h2>
            <div className={styles.lastNew}>
                {lastNews ? lastNews.text : 'Загрузка...'}
            </div>
        </div>
    );
}

export default LastNews;
