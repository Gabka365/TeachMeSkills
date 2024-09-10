import { useCallback, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import blogRepository from '../../../repositories/blogRepository';
import PostModel from '../../../models/PostModel';


function CreatePost() 
{
    let navigate = useNavigate();

    const { add } = blogRepository;

    const[name, setName] = useState<string>('');
    const[message, setMessage] = useState<string>('');

    const onNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setName(e.target.value);
        },
        []
    );


    const onMessageChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMessage(e.target.value);
        },
        []
    );

    const onCreate = useCallback(() => {
        add({ name, message } as PostModel).then((answer) => {
            if (answer.data) {
                navigate('/blog');
            } else {
                console.log('error');
            }
        });
    }, [name, message]);

    return (
        <div>
            <div>
                Name:
                <input type="text" value={name} onChange={onNameChange} />
            </div>
            <div>
                Message:
                <input
                    type="text"
                    value={message}
                    onChange={onMessageChange}
                />
            </div>
            <button onClick={onCreate}>Create</button>
        </div>
    );
}


export default CreatePost;
