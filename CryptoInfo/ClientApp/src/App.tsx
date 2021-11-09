import React from 'react';
import './App.css';
import FileUpload from "./components/FileUpload";
import TransactionList from "./components/TransactionList";
import WalletDetails from "./components/WalletDetails";
import {Stack, Grid, Container, CssBaseline} from "@mui/material";
import LeftCorner from "./components/LeftCorner";
import RightCorner from "./components/RightCorner";

function App() {
  return (
      <React.Fragment>
          <CssBaseline />
          <div className="App">
              <div>
                  <h1>CryptoInfo</h1>
              </div>
              <Container maxWidth="xl">
                  <Grid container spacing={2}>
                      <Grid item xs={12}>
                          <Stack direction="row" spacing={2} justifyContent="space-between" alignItems="center">
                              <LeftCorner />
                              <RightCorner />
                          </Stack>
                      </Grid>
                      <Grid item xs={12}>
                          <TransactionList />
                      </Grid>
                  </Grid>
              </Container>
          </div>
      </React.Fragment>
  );
}

export default App;
