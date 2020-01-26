import React, { useContext, useEffect } from "react";
import { Card, Image, Button, Grid } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import ActivityStore from "../../../app/stores/activityStore";
import { RouteComponentProps, Link } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import ActivityDetailedHeader from "./ActivityDetailedHeader";
import ActivityDetailedInfo from "./ActivityDetailedInfo";
import ActivityDetailedChat from "./ActivityDetailedChat";
import ActivityDetailedSideBar from "./ActivityDetailedSideBar";
interface DetailParams {
  id: string;
}

const ActivityDetails: React.FC<RouteComponentProps<DetailParams>> = ({
  match
}) => {
  const activityStore = useContext(ActivityStore);
  const { activity, loadActivity, loadingInitial } = activityStore;
  useEffect(() => {
    loadActivity(match.params.id);
  }, [loadActivity, match.params.id]);
  //return <h1>ActivityDetails</h1>;
  if (loadingInitial || !activity)
    return <LoadingComponent content="Loading Activity" />;
  return (
    <Grid>
      <Grid.Column width={10}>
        <ActivityDetailedHeader activity={activity} />
        <ActivityDetailedInfo activity={activity} />
        <ActivityDetailedChat />
      </Grid.Column>
      <Grid.Column width={6}>
        <ActivityDetailedSideBar />
      </Grid.Column>
    </Grid>
  );
};

export default observer(ActivityDetails);
