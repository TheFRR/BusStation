import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemAvatar from '@material-ui/core/ListItemAvatar';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemSecondaryAction from '@material-ui/core/ListItemSecondaryAction';
import ListItemText from '@material-ui/core/ListItemText';
import Avatar from '@material-ui/core/Avatar';
import IconButton from '@material-ui/core/IconButton';
import FormGroup from '@material-ui/core/FormGroup';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import FolderIcon from '@material-ui/icons/Folder';
import DeleteIcon from '@material-ui/icons/Delete';
import { AccessTime, AddLocation, Phone, Report } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    maxWidth: 752,
  },
  demo: {
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(4, 0, 2),
  },
}));

function generate(element) {
  return [0, 1, 2].map((value) =>
    React.cloneElement(element, {
      key: value,
    }),
  );
}

export default function InteractiveList() {
  const classes = useStyles();
  const [dense, setDense] = React.useState(false);
  const [secondary, setSecondary] = React.useState(false);

  return (
    <div className={classes.root}> 
      <Grid container spacing={2}>
        <Grid item xs={12} md={6}>
          <Typography variant="h6" className={classes.title}>
            Полезная информация и новости
          </Typography>
          <div className={classes.demo}>
            <List>
                <Button 
                variant="contained"
                color="primary"
                size="medium"
                style={{  width: '100%', margin: '10px auto' }}>История предприятия</Button>
                <Button 
                variant="contained"
                color="primary"
                size="medium"
                style={{  width: '100%', margin: '10px auto' }}>Информация о коронавирусе</Button>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Внимание! Вторая станция закрыта на ремонт. Отправление автобусов временно происходит от первой станции"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Report />
                  </ListItemIcon>
                  <ListItemText
                    primary="Рейсы до с. Жабное отменены"
                  />
                </ListItem>
            </List>
          </div>
        </Grid>
      </Grid>
    </div>
  );
}