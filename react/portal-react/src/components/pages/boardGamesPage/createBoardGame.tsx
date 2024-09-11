import { useCallback, useState } from 'react';
import { boardGameRepository } from '../../../repositories';
import BoardGameCreateViewModel from '../../../models/boardGames/BoardGameCreateViewModel';
import { useNavigate } from 'react-router-dom';
import './createBoardGame.css';

function CreateBoardGame() {
    let navigate = useNavigate();
    const { add } = boardGameRepository;
    const [boardGame, setboardGame] = useState<BoardGameCreateViewModel>(
        {} as BoardGameCreateViewModel
    );

    const onTitleChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, title: e.target.value };
            });
        },
        []
    );

    const onMiniTitleChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, miniTitle: e.target.value };
            });
        },
        []
    );

    const onDescriptionChange = useCallback(
        (e: React.ChangeEvent<HTMLTextAreaElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, description: e.target.value };
            });
        },
        []
    );

    const onEssenceChange = useCallback(
        (e: React.ChangeEvent<HTMLTextAreaElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, essence: e.target.value };
            });
        },
        []
    );

    const onTagsChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, tags: e.target.value };
            });
        },
        []
    );

    const onPriceChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, price: Number(e.target.value) };
            });
        },
        []
    );

    const onProductCodeChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setboardGame((oldBoardGame) => {
                return { ...oldBoardGame, productCode: Number(e.target.value) };
            });
        },
        []
    );

    const onCreate = useCallback(() => {
        add(boardGame as BoardGameCreateViewModel).then((answer) => {
            if (answer.data) {
                navigate('/boardGame');
            } else {
                console.log('error');
            }
        });
    }, [boardGame]);

    return (
        <div className="board-game-or-review-form">
            <h2>Добавьте новую игру</h2>
            <div>
                <div>
                    <input
                        className="foarm-field"
                        data-val="true"
                        data-val-required="Название не может быть пустым"
                        id="Title"
                        name="Title"
                        placeholder="Название игры"
                        type="text"
                        value={boardGame.title}
                        onChange={onTitleChange}
                        data-twofas-input="true"
                    />
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="Title"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div>
                    <input
                        className="foarm-field"
                        data-val="true"
                        data-val-required="Слоган не может быть пустым"
                        id="MiniTitle"
                        name="MiniTitle"
                        placeholder="Слоган игры"
                        type="text"
                        value={boardGame.miniTitle}
                        onChange={onMiniTitleChange}
                        data-twofas-input="true"
                    />
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="MiniTitle"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div className="file-input-container">
                    <p>Главное изображение</p>
                    <input
                        type="file"
                        name="MainImage"
                        accept="image/png, image/jpeg"
                    />
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="MainImage"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div className="file-input-container">
                    <p>Боковое изображение</p>
                    <input
                        type="file"
                        name="SideImage"
                        accept="image/png, image/jpeg"
                    />
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="SideImage"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div>
                    <textarea
                        className="foarm-field"
                        data-val="true"
                        data-val-required="Описание не может быть пустым"
                        id="Description"
                        name="Description"
                        placeholder="Описание игры"
                        value={boardGame.description}
                        onChange={onDescriptionChange}
                    ></textarea>
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="Description"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div>
                    <textarea
                        className="foarm-field"
                        id="Essence"
                        name="Essence"
                        placeholder="Суть игры"
                        value={boardGame.essence}
                        onChange={onEssenceChange}
                    ></textarea>
                </div>
                <div>
                    <input
                        className="foarm-field"
                        id="Tags"
                        name="Tags"
                        placeholder="Тег для игры"
                        type="text"
                        value={boardGame.tags}
                        onChange={onTagsChange}
                        data-twofas-input="true"
                    />
                </div>
                <div>
                    <input
                        className="foarm-field"
                        data-val="true"
                        data-val-number="The field Price must be a number."
                        data-val-required="Цена не может быть пустой"
                        id="Price"
                        name="Price"
                        placeholder="Цена игры"
                        type="number"
                        value={boardGame.price}
                        onChange={onPriceChange}
                        data-twofas-input="true"
                    />
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="Price"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div>
                    <input
                        className="foarm-field"
                        data-val="true"
                        data-val-required="Код товара не может быть пустым"
                        id="ProductCode"
                        name="ProductCode"
                        placeholder="Код товара"
                        type="number"
                        value={boardGame.productCode}
                        onChange={onProductCodeChange}
                        data-twofas-input="true"
                    />
                    <span
                        className="field-validation-valid"
                        data-valmsg-for="ProductCode"
                        data-valmsg-replace="true"
                    ></span>
                </div>
                <div>
                    <input
                        className="button-send"
                        type="submit"
                        value="Добавить игру"
                        onClick={onCreate}
                    />
                </div>
                <input
                    name="__RequestVerificationToken"
                    type="hidden"
                    value="CfDJ8LR2Fg3ir-tJtPb0X8xGCDfCjf-2zkICwjkii3YX2x8t8hD8cGZFzDGWvfi6kGdAzA_A3g3Dx0hWoEnLLJnAcaI4N4x30lYGu7uTkSHBIGb6054LMpZjCC62tNKkEV8FwUvuabq-7LPx-2OkmxhlNqNbRNrGCpqNgr_LNSYFD7-zAbW1tWaSEJ45BiD7I_-M-Q"
                />
            </div>
        </div>
    );
}

export default CreateBoardGame;
