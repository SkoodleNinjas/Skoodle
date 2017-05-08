package Application.room;

import Application.room.name.RoomName;
import Application.room.topic.Topic;

import javax.persistence.*;
import java.util.Random;

/**
 * Created by ivan on 12.4.2017 г..
 */

@Entity
public class Room {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;

    @OneToOne
    @PrimaryKeyJoinColumn
    private Topic topic;
    @OneToOne
    @PrimaryKeyJoinColumn
    private RoomName name;
    private long maxPlayers;
    private long[] playerIds;
    private long numberOfRounds;

    public Room() {
        generateNumberOfRounds();
    }

    private void generateNumberOfRounds() {
        Random random = new Random();
        numberOfRounds = random.nextInt((7 - 3 + 1)) + 3;
    }

    public long getId() {
        return id;
    }

    public long getMaxPlayers() {
        return maxPlayers;
    }

    public void setMaxPlayers(long maxPlayers) {
        this.maxPlayers = maxPlayers;
    }

    public Topic getTopic() {
        return topic;
    }

    public void setTopic(Topic topic) {
        this.topic = topic;
    }

    public long[] getPlayerIds() {
        return playerIds;
    }

    public String getName() {
        return name.getRoomName();
    }

    public void setName(RoomName name) {
        this.name = name;
    }

    public long getNumberOfRounds() {
        return  numberOfRounds;
    }

    public void setNumberOfRounds(long numberOfRounds) {
        this.numberOfRounds = numberOfRounds;
    }

}
