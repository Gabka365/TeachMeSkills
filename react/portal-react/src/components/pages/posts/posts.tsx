import Post from "./post"
import { useEffect, useState } from "react"
import blogRepository from "../../../repositories/blogRepository"
import PostModel from "../../../models/PostModel"

function Posts()
{
    const { getAll } = blogRepository;
    const [Posts, setPosts] = useState<PostModel[]>([])
    
    useEffect(() => {
        getAll()
            .then(Posts => {
                setPosts(Posts.data)
            }
        );
    }, [])


    return(
        <div className="posts">
            {Posts.map(post => 
                <Post 
                    name={post.name}
                    dateOfPublich={post.dateOfPublish}
                    key={post.id}
                    message={post.message}
                ></Post>)}
        </div>
    )
}


export default Posts