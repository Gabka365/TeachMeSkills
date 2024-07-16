import React, { useCallback, useState } from 'react';
import  repository  from '../../../repositories/travelingRepsoitory'
import { useNavigate } from 'react-router-dom';

function CreateTravelings() {
    const [selectedFile, setSelectedFile] = useState<File | null>(null);
    const [name, setName] = useState<string>('');
    const [desc, setDesc] = useState<string>('');
    const { create } = repository;
    const navigate = useNavigate(); 

    const onNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setName(e.target.value);
        },
        []
    );

    const onDescChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setDesc(e.target.value);
        },
        []
    );

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.files) {
            setSelectedFile(event.target.files[0]);
        }
    };

    const handleUpload = () => {
        const formData = new FormData();
        if (selectedFile) {
            formData.append('image', selectedFile);
        }
        formData.append('name', name);
        formData.append('desc', desc);

       create(formData)
           .then(() => {
               // Перенаправление после успешного создания
               navigate('/traveling');
           })
           .catch((error) => {
               console.error('Error creating traveling:', error);
           });
    };
    
    return (
        <div>
            <p>Здесь можно создать пост про путешествие</p>
            <div>
                Имя:
                <input type="text" value={name} onChange={onNameChange} />
            </div>
            <div>
                Описание:
                <input type="text" value={desc} onChange={onDescChange} />
            </div>
            <div>
                Картинка:
                <input type="file" onChange={handleFileChange} />
            </div>
            <button onClick={handleUpload}>Загрузить</button>
        </div>
    );
}

export default CreateTravelings;
