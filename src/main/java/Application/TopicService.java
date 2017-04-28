package Application;

import java.util.List;

/**
 * Created by nikolay on 4/27/17.
 */
public interface TopicService {
    public List<Topic> findAll();
    public Topic findOne(long id);
    public Topic findRandom();
    public void saveTopic(Topic topic);
}
