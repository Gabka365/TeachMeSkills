import React from 'react';
import styles from './traveling.module.css';
import LastNews from '../lastNews/LastNews';

function Traveling(){

  
    return(
        <div className={styles.traveling}>
           <LastNews></LastNews>
        </div>
    )
}

export default Traveling