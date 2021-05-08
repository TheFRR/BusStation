import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import { AccessTime, AddLocation, Phone } from '@material-ui/icons';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
    maxWidth: 750,
  },
  demo: {
    backgroundColor: theme.palette.background.paper,
  },
  title: {
    margin: theme.spacing(4, 0, 2),
  },
}));

export default function InteractiveList() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Grid container>
        <Grid item style={{width: '300px', height: '500px', marginLeft: 'auto', marginRight: 'auto' }}>
          <Typography variant="h6" className={classes.title} align="left">
            Данные о работе автовокзала
          </Typography>
          <div className={classes.demo}>
            <List>
                <ListItem>
                  <ListItemIcon>
                    <Phone />
                  </ListItemIcon>
                  <ListItemText
                    primary="Справочная автовокзала: 23-41-54"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Phone />
                  </ListItemIcon>
                  <ListItemText
                    primary="Диспетчерская: 23-41-41"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Phone />
                  </ListItemIcon>
                  <ListItemText
                    primary="Касса по продаже билетов: 57-68-99"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <Phone />
                  </ListItemIcon>
                  <ListItemText
                    primary="Предварительный заказ билетов: 57-68-98"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <AddLocation />
                  </ListItemIcon>
                  <ListItemText
                    primary="Адрес автовокзала: 000001, г. Жабинск, ул. Квакинская, д. 152"
                  />
                </ListItem>
                <ListItem>
                  <ListItemIcon>
                    <AccessTime />
                  </ListItemIcon>
                  <ListItemText
                    primary="Режим работы: ежедневно, круглосуточно"
                  />
                </ListItem>
            </List>
          </div>
        </Grid>
      </Grid>
    </div>
  );
}