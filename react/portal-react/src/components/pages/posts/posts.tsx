import Post from './post';
import { useCallback, useEffect, useState } from 'react';
import blogRepository from '../../../repositories/blogRepository';
import PostModel from '../../../models/PostModel';
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';

function Posts() {
    const { getAll } = blogRepository;
    const [Posts, setPosts] = useState<PostModel[]>([]);

    useEffect(() => {
        getAll().then((Posts) => {
            setPosts(Posts.data);
        });
    }, []);

    const onPostDelete = useCallback((id: number) => {
        setPosts((oldPosts) => [...oldPosts.filter((p) => p.id !== id)]);
    }, []);

    return (
        <div>
            <Link to={'/blog/create'}>Create Post</Link>
            <div className="posts">
                {Posts.map((post) => (
                    <div>
                        <Post
                            PostBody={post}
                            OnDelete={onPostDelete}
                            key={post.id}
                        ></Post>
                        <button>
                            {' '}
                            <Link to={'/blog/update'}> Update Post</Link>
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Posts;
