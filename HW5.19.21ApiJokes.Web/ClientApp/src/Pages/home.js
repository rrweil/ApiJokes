import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';
import useInterval from '../Hooks/UseInterval'


const Home = () => {
    const [joke, setJoke] = useState({id:'', setup:'', punchline:'',likesCount:null,dislikesCount:null});
    const [loading, setLoading] = useState(true);
    const { user } = useAuthContext();
    const [likeButtonStatus, setLikeButtonStatus] = useState([]);


    useEffect(() => {
        const generateJoke = async () => {
            const { data } = await axios.get('/api/joke/getjoke');
            setJoke(data);
            getLikeButtonStatus(data.id);
            setLoading(false);
        }
        generateJoke();
    }, []);

    useInterval(() => getCounts(joke.id), 1000)

    const getLikeButtonStatus = async (jokeId) => {
        const {data} = await axios.get(`/api/joke/getLikeButtonStatus?jokeId=${jokeId}`);
        setLikeButtonStatus(data);
    }

    const getCounts = async (jokeId) => {
        const { data } = await axios.get(`/api/joke/getcounts?jokeId=${jokeId}`);
        const copy = {...joke, likesCount: data.likeCounts, dislikesCount: data.dislikeCounts };
        setJoke(copy);
    }

    const refreshPage = () => {
        setLoading(true);
        window.location.reload();
    }

    const updateLikeStatus = async e => {
        const liked = e.target.name == "Like" ? true : false;
        await axios.post('/api/joke/likejoke', { jokeId: joke.id, liked });
        getLikeButtonStatus(joke.id);
    }

    return (
        <div className="row"><div className="col-md-6 offset-md-3 card card-body bg-light">
            <div>
                {loading && <h4>Loading...</h4>}
                {!loading &&
                    <>
                        <h4>{joke.setup}</h4>
                        <h3>{joke.punchline}</h3>
                        <div>
                            {!user &&
                                <div>
                                    <a href="/login">Login to your account to like/dislike this joke</a>
                                </div>}
                            {!!user &&
                                <div className="mt-3">
                                    <button 
                                    onClick={updateLikeStatus} 
                                    disabled={likeButtonStatus=="TimeElapsedButtonsLocked" || likeButtonStatus =="Liked"} 
                                    name="Like" 
                                    className="btn btn-primary mr-2">Like</button>
                                    <button onClick={updateLikeStatus} 
                                    disabled={likeButtonStatus=="TimeElapsedButtonsLocked" || likeButtonStatus =="Disliked"} 
                                    name="Dislike" 
                                    className="btn btn-danger">Dislike</button>
                                </div>
                            }
                            <br></br>
                            <h4>Likes: {joke.likesCount}</h4>
                            <h4>Dislikes: {joke.dislikesCount}</h4>
                            <h4>
                                <button onClick={refreshPage} className="pl-0 btn btn-link">Refresh</button>
                            </h4>
                        </div>
                    </>
                }
            </div>
        </div>
        </div>

    );

}

export default Home;