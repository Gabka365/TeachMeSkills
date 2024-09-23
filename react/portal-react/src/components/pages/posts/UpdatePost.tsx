import { Navigate, useNavigate, useParams } from 'react-router-dom';
import blogRepository from '../../../repositories/blogRepository';
import PostModel from '../../../models/PostModel';
import { useCallback, useEffect, useState } from 'react';
import Post from './post';
import Permission from '../../../contexts/Permission';

function UpdatePost() {
    let navigate = useNavigate();
    const { postId } = useParams();
    const [post, setPost] = useState<PostModel>({} as PostModel);
    const { get, update } = blogRepository;

    useEffect(() => {
        get(+postId!).then((response) => {
            setPost(response.data as PostModel);
        });
    }, []);

    const onNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setPost((oldPost) => {
                return { ...oldPost, name: e.target.value };
            });
        },
        []
    );

    const onMessageChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setPost((oldPost) => {
                return { ...oldPost, message: e.target.value };
            });
        },
        []
    );

    const onUpdate = useCallback(() => {
        update(post as PostModel).then((answer) => {
            if (answer.data) {
                navigate('/blog');
            } else {
                console.log('error');
            }
        });
    }, [post]);

    return (
        <Permission check={(p) => p.CanUpdateInBlog}>
            <div>
                <div>
                    Name:
                    <input
                        type="text"
                        value={post.name}
                        onChange={onNameChange}
                    />
                </div>
                <div>
                    Message:
                    <input
                        type="text"
                        value={post.message}
                        onChange={onMessageChange}
                    />
                </div>
                <div>
                    <input type="hidden" value={postId} />
                </div>
                <button onClick={onUpdate}>Update</button>
            </div>
        </Permission>
    );
}

export default UpdatePost;
