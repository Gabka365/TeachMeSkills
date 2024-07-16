import { useParams } from 'react-router-dom';
import { boardGameRepository } from '../../../repositories';
import { useEffect, useState } from 'react';
import BoardGameViewModel from '../../../models/boardGames/BoardGameViewModel';
import { BASE_API_URL } from '../../../repositories/apiConstatns';
import './boardGameDatails.css'

function BoardGameDetails() {
    const { id } = useParams();
    const { get } = boardGameRepository;
    const [boardGame, setBoardGame] = useState<BoardGameViewModel>();

    useEffect(() => {
        if (id) {
            get(+id).then((response) => {
                setBoardGame(response.data as BoardGameViewModel);
            });
        } else {
            console.error('There no ID');
        }
    }, []);

    return (
        <div>
            {!boardGame && <div>loading</div>}

            {!!boardGame && (
                <div>
                    <header className="board-game-header">
                        <div className="container">
                            <div className="header-elements">
                                <div className="left-elements">
                                    <a href="#" className="header-element">
                                        Магазины
                                    </a>
                                    <a href="#" className="header-element">
                                        Игротеки
                                    </a>
                                </div>
                                <div className="right-elements">
                                    <a href="#" className="header-element">
                                        График работы
                                    </a>
                                    <p className="header-element">
                                        +7 123 456-78-90
                                    </p>
                                </div>
                            </div>
                        </div>
                    </header>

                    <input
                        type="hidden"
                        className="game-id"
                        value={boardGame.id}
                    />
                    <input
                        type="hidden"
                        className="text-for-add"
                        value="В избранное"
                    />
                    <input
                        type="hidden"
                        className="text-for-remove"
                        value="Удалить из избранного"
                    />
                    <input
                        type="hidden"
                        className="absence-of-reviews-text"
                        value="Отзывов пока нет. Оставьте первый!"
                    />

                    <div className="title">
                        <div className="container">
                            <h1 className="head-title">{boardGame.title}</h1>
                            <p className="mini-title">{boardGame.miniTitle}</p>
                        </div>
                    </div>

                    <div className="main-block">
                        <div className="container">
                            <div className="main-wraper">
                                <div className="left-part">
                                    <div className="images">
                                        {boardGame.hasSideImage && (
                                            <img
                                                src={`${BASE_API_URL}images/BoardGame/sideImage-${boardGame.id}.jpg`}
                                                alt="Боковые фото"
                                                className="elongated-image"
                                            />
                                        )}
                                        {boardGame.productCode === 31458 && (
                                            <img
                                                src={`${BASE_API_URL}images/BoardGame/sideImage-default.png`}
                                                alt="Боковые фото"
                                                className="elongated-image"
                                            />
                                        )}
                                        {boardGame.hasMainImage && (
                                            <img
                                                src={`${BASE_API_URL}images/BoardGame/mainImage-${boardGame.id}.jpg`}
                                                alt="Фото игры"
                                                className="square-image"
                                            />
                                        )}
                                        {boardGame.productCode === 31458 && (
                                            <img
                                                src={`${BASE_API_URL}images/BoardGame/mainImage-default.jpg`}
                                                alt="Фото игры"
                                                className="square-image"
                                            />
                                        )}
                                    </div>
                                    <div className="description">
                                        <h3 className="desc-title">Описание</h3>
                                        <p className="desc-text">
                                            {boardGame.description}
                                        </p>
                                        {!!boardGame.essence && (
                                            <div>
                                                <h3 className="desc-title">
                                                    Суть игры
                                                </h3>
                                                <p className="desc-text">
                                                    {boardGame.essence}
                                                </p>
                                            </div>
                                        )}
                                    </div>
                                    {!!boardGame.tags && (
                                        <div className="tags">
                                            <span className="tag">
                                                {boardGame.tags}
                                            </span>
                                        </div>
                                    )}
                                </div>
                                <div className="price-card">
                                    <div className="first-row">
                                        <h2 className="price">
                                            {boardGame.price} ₽
                                        </h2>
                                        <p className="code">
                                            Код товара
                                            {boardGame.productCode}
                                        </p>
                                    </div>
                                    <div className="buttons">
                                        <button className="highlighted-button">
                                            Добавить в корзину
                                        </button>
                                        {!!boardGame.isFavoriteForUser && (
                                            <div>
                                                <input
                                                    type="hidden"
                                                    className="is-add"
                                                    value="0"
                                                />
                                                <button className="usual-button favorite-button">
                                                    Удалить из избранного
                                                </button>
                                            </div>
                                        )}
                                        {!boardGame.isFavoriteForUser && (
                                            <div>
                                                <input
                                                    type="hidden"
                                                    className="is-add"
                                                    value="1"
                                                />
                                                <button className="usual-button favorite-button">
                                                    В избранное
                                                </button>
                                            </div>
                                        )}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div className="reviews">
                        <div className="container">
                            <h3 className="reviews-head">Отзывы</h3>
                            <div className="reviews-container"></div>
                            <div className="review-buttons">
                                <button className="usual-button">
                                    Показать ещё
                                </button>
                                <button className="highlighted-button create-review-button">
                                    Написать отзыв
                                </button>
                            </div>
                        </div>
                    </div>

                    <div className="board-game-footer">
                        <div className="container">
                            <div className="footer-elements">
                                <p>Производитель - hobbygames</p>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}

export default BoardGameDetails;
