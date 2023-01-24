import React, { useEffect, useState } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText } from 'reactstrap';
import { searchVideos } from "../modules/videoManager.js";

export const VideoSearch = (videos) => {
    const [searchString, setSearchString] = useState("");
    const [descending, setDescending] = useState(false);
   
    const search = () => {
        searchVideos(searchString, descending).then((videos) => setSearchString(videos));
        
    };

    useEffect(() => {
        searchVideos(searchString, descending);
    }, [videos])
    
    return (
        <Form>
        <Label for="searchString">Search: </Label>
        <Input type="search" name="searchString" onChange={(event) => setSearchString(event.target.value)} />
        <Label for="descending">Sort Descending? </Label>
        <Input type="checkbox" name="descending" onChange={(event) => setDescending(event.target.checked)} />
        <Button onClick={() => search()}>Search</Button>
    </Form>
        );
};

export default VideoSearch;

