package Application;

import org.springframework.beans.factory.annotation.Autowired;

import javax.persistence.*;

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
    private long maxPlayers;
    private long[] playerIds;
    private String name;
    private long numberOfRounds;

    public Room() {
        generateName();
        generateNumberOfRounds();
    }

    private void generateName() {
        name = "Qkata rabota";
    }

    private void generateNumberOfRounds() {
        numberOfRounds = 5;
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
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public long getNumberOfRounds() {
        return  numberOfRounds;
    }

    public void setNumberOfRounds(long numberOfRounds) {
        this.numberOfRounds = numberOfRounds;
    }

}
