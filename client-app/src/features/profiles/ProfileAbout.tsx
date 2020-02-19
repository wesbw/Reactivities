import React, { useState, useContext } from "react";
import { observer } from "mobx-react-lite";
import { Form, Tab, Grid, Header, Button } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { Form as FinalForm, Field } from "react-final-form";
import { combineValidators, isRequired } from "revalidate";
import TextInput from "../../app/common/form/TextInput";
import TextAreaInput from "../../app/common/form/TextAreaInput";

const validate = combineValidators({
  displayName: isRequired("displayName")
});

function ProfileAbout() {
  const rootStore = useContext(RootStoreContext);
  const { editProfile, profile, isCurrentUser } = rootStore.profileStore;
  const [editMode, setEditMode] = useState(false);
  return (
    <Tab.Pane>
      <Grid>
        <Grid.Column width={16}>
          <Header floated="left" icon="user" content="About" />
          {isCurrentUser && (
            <Button
              floated="right"
              basic
              content={editMode ? "Cancel" : "Edit Profile"}
              onClick={() => setEditMode(!editMode)}
            />
          )}
        </Grid.Column>
        <Grid.Column width={16}>
          {editMode ? (
            <FinalForm
              onSubmit={editProfile}
              validate={validate}
              initialValues={profile!}
              render={({ handleSubmit, invalid, pristine, submitting }) => (
                <Form onSubmit={handleSubmit} error>
                  <Field
                    name="displayName"
                    component={TextInput}
                    placeholder="Display Name"
                    value={profile!.displayName}
                  />
                  <Field
                    name="bio"
                    component={TextAreaInput}
                    rows={3}
                    placeholder="Bio"
                    value={profile!.bio}
                  />
                  <Button
                    loading={submitting}
                    floated="right"
                    disabled={invalid || pristine}
                    positive
                    content="Update profile"
                  />
                </Form>
              )}
            />
          ) : (
            <span>{profile!.bio}</span>
          )}
        </Grid.Column>
      </Grid>
    </Tab.Pane>
  );
}

export default observer(ProfileAbout);
