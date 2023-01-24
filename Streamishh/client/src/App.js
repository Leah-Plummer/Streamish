import React from "react";
import "./App.css";
import  VideoForm  from "./components/VideoForm";
import VideoList from "./components/VideoList";
import VideoSearch from "./components/SearchVideos";

function App() {
  return (
    <div className="App">
      <VideoForm />
      <VideoSearch/>
      <VideoList />
    </div>
  );
}

export default App;