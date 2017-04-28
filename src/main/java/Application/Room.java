package Application;

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
}
