import { FC, useCallback } from "react"
import PostModel from "../../../models/PostModel"
import blogRepository from "../../../repositories/blogRepository"


interface PostProp{
    PostBody: PostModel
    OnDelete: (id: number) => void
}

const Post: FC<PostProp> = (post) =>
{

    const Remove = useCallback((id: number) => {
        blogRepository.remove(id);
        post.OnDelete(id);
    }, [])

    return (
        <div className="post">
            The text "{post.PostBody.message}" published by user {post.PostBody.name} on {post.PostBody.dateOfPublish}.
            <button onClick={() => Remove(post.PostBody.id)}> Remove </button>
        </div>
    )
}

export default Post