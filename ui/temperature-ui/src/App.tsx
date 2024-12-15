import "./App.css";
import "./styles/main.scss";
import Register from "./components/Register/Register";
import Login from "./components/Login/Login";
import { useEffect } from "react";
import { setAuthorizationHeader } from "./api/serviceHelper";
import {
  BrowserRouter,
  Route,
  Routes,
} from "react-router-dom";
import { PublicRoute, PrivateRoute } from "./routes";
import Home from "./components/Home/Home";

function App() {
  useEffect(() => {
    const token = localStorage.getItem("authToken");
    setAuthorizationHeader(token);
  }, []);

  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route element={<PublicRoute />}>
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Route>

          <Route element={<PrivateRoute />}>
            <Route path="/" element={<Home />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;