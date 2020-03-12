
<script>
    import Layout from '../Shared/Layout.svelte'
    import {user, reglament} from '../store'

    
</script>

<Layout>
    <div class="container">
        <h5>user</h5>
        <p>
            {JSON.stringify($user)}
        </p>

        <h5>reglament</h5>
        <p>
            {JSON.stringify($reglament)}
        </p>

    </div>


</Layout>
