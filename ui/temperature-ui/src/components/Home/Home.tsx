import Dashboard from "../Dashboard/Dashboard";
import Footer from "../Footer/Footer";
import Header from "../Header/Header";
import React from "react";

const Home: React.FC = () => {
  return (
    <div>
      <Header />
      <Dashboard />
      <Footer />
    </div>
  );
};

export default Home;
