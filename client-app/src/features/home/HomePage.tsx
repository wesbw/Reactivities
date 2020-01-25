import React from "react";
import { Container } from "semantic-ui-react";
import { Link } from "react-router-dom";

const HomePage = () => {
  return (
    <Container style={{ marginTop: "7em" }}>
      <h1>HomePage</h1>
      <h3>
        Go to <Link to="/activities">activities</Link>
      </h3>
    </Container>
  );
};

export default HomePage;
